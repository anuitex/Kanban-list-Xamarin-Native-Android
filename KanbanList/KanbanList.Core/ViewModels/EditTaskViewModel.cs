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
using System.Linq;
using System.Threading.Tasks;
using KanbanList.Core.Repositories.Interfaces;

namespace KanbanList.Core.ViewModels
{
    public class EditTaskViewModel : BaseViewModel<TaskModelResult, DestructionResult<TaskModelResult>>
    {

        #region Variables

        private List<ImageFileModelResult> _deletedImages = new List<ImageFileModelResult>();
        private Dictionary<string, Action> _choosePiickerOptions = new Dictionary<string, Action>();
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IValidationService _validationService;
        private readonly IImageRepository<ImageModelEntity> _imageRepository;
        private readonly IUserRepository<UserModelEntity> _userRepository;

        #endregion Variables

        #region Constructors

        public EditTaskViewModel(
            IMapper mapper,
            IPhotoService photoService,
            IValidationService validationService,
            IImageRepository<ImageModelEntity> imageRepository,
            IUserRepository<UserModelEntity> userRepository)
        {
            _mapper = mapper;
            _photoService = photoService;
            _validationService = validationService;

            _choosePiickerOptions.Add("Take Photo", async () => await TakePhoto());
            _choosePiickerOptions.Add("Library", async () => await PickPhoto());

            _imageRepository = imageRepository;
            _userRepository = userRepository;
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
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            List<ImageModelEntity> imageEntities = await _imageRepository.GetAllImagesList(TaskItem.Id);
            if (imageEntities != null)
            {
                var images = _mapper.Map<List<ImageModelEntity>, List<ImageFileModelResult>>(imageEntities);

                images.ForEach(x => { x.DeleteImage = DeleteAttachedImage; });

                await InvokeOnMainThreadAsync(() =>
                {
                    AttachedFiles.AddRange(images);
                });
            }

            List<UserModelEntity> userEntities = await _userRepository.GetAllUsersList();
            var users = _mapper.Map<List<UserModelEntity>, List<UserModelResult>>(userEntities);
            if (users != null)
            {
                AssignedUserList.AddRange(users);
                AssignedUser = AssignedUserList.FirstOrDefault(x => x.Id == TaskItem.AssignedUserId);
            }
        }

        #endregion Overrides

        #region Commands  

        public IMvxCommand SaveTaskCommand => new MvxCommand(async () =>
        {
            var validationResult = _validationService.ValidateModel(TaskItem);
            if (!validationResult.IsValid)
            {
                await UserDialogs.AlertAsync("Input fields haven't to be empty", "Action error");
                return;
            }

            var imageEntiities = _mapper.Map<List<ImageFileModelResult>, List<ImageModelEntity>>(_deletedImages);

            foreach (var image in imageEntiities)
            {

                if (!string.IsNullOrEmpty(image.FilePath) && File.Exists(image.FilePath))
                {
                    File.Delete(image.FilePath);
                }
                await _imageRepository.Delete(image);
            }

            TaskItem.CreatedDate = DateTime.Now;
            await NavigationService.Close(this, new DestructionResult<TaskModelResult>
            {
                Entity = TaskItem,
                Destroyed = false
            });
        });

        public IMvxCommand DeleteTaskCommand => new MvxCommand(async () =>
        {
            var result = await UserDialogs.ConfirmAsync($"Do you want delete this task?", "Action");
            await NavigationService.Close(this, new DestructionResult<TaskModelResult>
            {
                Entity = TaskItem,
                Destroyed = result
            });
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
            UserDialogs.ShowLoading();
            ImageFileModelResult image = await _photoService.TakePhoto();
            if (image != null)
            {
                SaveImage(image);
            }
            UserDialogs.HideLoading();
        }

        public async Task PickPhoto()
        {
            UserDialogs.ShowLoading();
            ImageFileModelResult image = await _photoService.PickPhoto();
            if (image != null)
            {
                SaveImage(image);
            }
            UserDialogs.HideLoading();
        }

        private async void SaveImage(ImageFileModelResult image)
        {
            image.Id = Guid.NewGuid().ToString();
            image.TaskId = TaskItem.Id;
            image.DeleteImage = DeleteAttachedImage;
            AttachedFiles.Add(image);

            await RaisePropertyChanged(() => AttachedFiles);

            var imageEntity = _mapper.Map<ImageFileModelResult, ImageModelEntity>(image);

            _imageRepository.Create(imageEntity);
        }

        public async void DeleteAttachedImage(ImageFileModelResult image)
        {
            bool alertResult = await UserDialogs.ConfirmAsync($"Do you want delete this image?", "Action");
            if (!alertResult)
            {
                return;
            }

            AttachedFiles.Remove(image);
            _deletedImages.Add(image);

        }

        #endregion Methods
    }
}