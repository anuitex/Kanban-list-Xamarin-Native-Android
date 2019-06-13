using AutoMapper;
using KanbanList.Core.Entities;
using KanbanList.Core.Extensions;
using KanbanList.Core.Models;
using KanbanList.Core.Services.Interfaces;
using KanbanList.Core.ViewModels.Base;
using MvvmCross;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using KanbanList.Core.Repositories.Interfaces;

namespace KanbanList.Core.ViewModels
{
    public class CreateTaskViewModel : BaseViewModel<TaskModelResult, DestructionResult<TaskModelResult>>
    {
        #region Variables

        private Dictionary<string, Action> _choosePiickerOptions = new Dictionary<string, Action>();

        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;
        private readonly IImageRepository<ImageModelEntity> _imageRepository;
        private readonly IUserRepository<UserModelEntity> _userRepository;
        private readonly IPhotoService _photoService;

        #endregion Variables

        #region Constructors

        public CreateTaskViewModel(
            IMapper mapper,
            IPermissionService permissionService,
            IPhotoService photoService,
            IValidationService validationService,
            IImageRepository<ImageModelEntity> imageRepository,
            IUserRepository<UserModelEntity> userRepository)
        {
            _mapper = mapper;
            _photoService = photoService;
            _imageRepository = imageRepository;
            _userRepository = userRepository;
            _validationService = validationService;

            _choosePiickerOptions.Add("Take Photo", async () => await TakePhoto());
            _choosePiickerOptions.Add("Library", async () => await PickPhoto());
        }

        #endregion Constructors

        #region Properties

        private UserModelResult _assignedUser;
        public UserModelResult AssignedUser
        {
            get => _assignedUser;
            set
            {
                SetProperty(ref _assignedUser, value);
                TaskItem.AssignedUserId = value.Id;
            }
        }

        private TaskModelResult _taskItem;
        public TaskModelResult TaskItem
        {
            get => _taskItem;
            set => SetProperty(ref _taskItem, value);
        }

        private ObservableCollection<ImageFileModelResult> _attachedFiles = new ObservableCollection<ImageFileModelResult>();
        public ObservableCollection<ImageFileModelResult> AttachedFiles
        {
            get => _attachedFiles;
            set => SetProperty(ref _attachedFiles, value);
        }

        private ObservableCollection<UserModelResult> _assignedUserList = new ObservableCollection<UserModelResult>();
        public ObservableCollection<UserModelResult> AssignedUserList
        {
            get => _assignedUserList;
            set => SetProperty(ref _assignedUserList, value);
        }

        #endregion Properties

        #region Overrides

        public override void Prepare(TaskModelResult parameter)
        {
            TaskItem = parameter;
            TaskItem.Id = Guid.NewGuid().ToString();
            TaskItem.TaskStatus = Enums.TaskStatus.Open;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            List<UserModelEntity> result = await _userRepository.GetAllUsersList();
            var users = _mapper.Map<List<UserModelEntity>, List<UserModelResult>>(result);

            if (users != null)
            {
                AssignedUserList.AddRange(users);
            }
        }

        #endregion Overrides

        #region Commands  

        public IMvxCommand CreateTaskCommand => new MvxCommand(async () =>
        {
            ValidationModelResult result = _validationService.ValidateModel(TaskItem);
            if (!result.IsValid)
            {
                await UserDialogs.AlertAsync("Input fields haven't to be empty", "Action error");
                return;
            }

            TaskItem.CreatedDate = DateTime.Now;

            await NavigationService.Close(this, new DestructionResult<TaskModelResult>
            {
                Entity = TaskItem,
                Destroyed = false
            });
        });

        public IMvxCommand CancelTaskCommand => new MvxCommand(async () =>
        {
            bool result = await UserDialogs.ConfirmAsync("If you exit all information has will be delete.\n Do you want exit?", "Notification");

            if (result)
            {
                _imageRepository.DeleteAllImages(TaskItem.Id);

                await NavigationService.Close(this);
            }
        });

        public IMvxCommand AttachImageCommand => new MvxCommand(() =>
        {
            var pickerDialog = Mvx.IoCProvider.Resolve<IPickerDialogService>();
            pickerDialog.ShowChoosePicker(_choosePiickerOptions);
        });

        #endregion Commands

        #region Methods

        public async Task TakePhoto()
        {
            ImageFileModelResult image = await _photoService.TakePhoto();
            if (image != null)
            {
                SaveImage(image);
            }
        }

        public async Task PickPhoto()
        {
            ImageFileModelResult image = await _photoService.PickPhoto();
            if (image != null)
            {
                SaveImage(image);
            }
        }

        private async void SaveImage(ImageFileModelResult image)
        {
            image.DeleteImage = DeleteAttachedImage;
            image.Id = Guid.NewGuid().ToString();
            image.TaskId = TaskItem.Id;
            AttachedFiles.Add(image);

            await RaisePropertyChanged(() => AttachedFiles);

            var imageEntity = _mapper.Map<ImageFileModelResult, ImageModelEntity>(image);

            _imageRepository.Create(imageEntity);
        }

        public async void DeleteAttachedImage(ImageFileModelResult model)
        {
            bool resultAlert = await UserDialogs.ConfirmAsync($"Do you want delete this image?", "Action");
            if (!resultAlert)
            {
                return;
            }

            if (!string.IsNullOrEmpty(model.FilePath) && File.Exists(model.FilePath))
            {
                File.Delete(model.FilePath);
            }

            AttachedFiles.Remove(model);
        }

        #endregion Methods
    }
}
