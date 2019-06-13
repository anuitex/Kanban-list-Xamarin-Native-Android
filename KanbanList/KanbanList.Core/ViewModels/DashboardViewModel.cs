using AutoMapper;
using KanbanList.Core.Configurations;
using KanbanList.Core.Entities;
using KanbanList.Core.Helpers;
using KanbanList.Core.Models;
using KanbanList.Core.ViewModels.Base;
using MvvmCross.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using KanbanList.Core.Repositories.Interfaces;

namespace KanbanList.Core.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {

        #region Variables

        private readonly IMapper _mapper;
        private readonly ITaskRepository<TaskModelEntity> _taskRepository;
        private readonly IImageRepository<ImageModelEntity> _imageRepository;

        #endregion Variables

        #region Constructors

        public DashboardViewModel(
            IImageRepository<ImageModelEntity> imageRepository, 
            ITaskRepository<TaskModelEntity> taskRepository, 
            IMapper mapper)
        {

            #region Init sections
            var openList = new SectionModelResult
            {
                Title = Constants.TitleOpen
            };

            var inProgressList = new SectionModelResult
            {
                Title = Constants.TitleInProgress
            };

            var resolvedList = new SectionModelResult
            {
                Title = Constants.TitleResolved
            };

            var verifiedList = new SectionModelResult
            {
                Title = Constants.TitleVerified
            };

            var completedList = new SectionModelResult
            {
                Title = Constants.TitleCompleted
            };

            openList.CurrentSectionStatus = Enums.TaskStatus.Open;
            openList.CanMoveTaskToList.Add(Enums.TaskStatus.InProgress);

            inProgressList.CurrentSectionStatus = Enums.TaskStatus.InProgress;
            inProgressList.CanMoveTaskToList.Add(Enums.TaskStatus.Open);
            inProgressList.CanMoveTaskToList.Add(Enums.TaskStatus.Resolved);

            resolvedList.CurrentSectionStatus = Enums.TaskStatus.Resolved;
            resolvedList.CanMoveTaskToList.Add(Enums.TaskStatus.Verified);
            resolvedList.CanMoveTaskToList.Add(Enums.TaskStatus.Completed);

            verifiedList.CurrentSectionStatus = Enums.TaskStatus.Verified;
            verifiedList.CanMoveTaskToList.Add(Enums.TaskStatus.Open);
            verifiedList.CanMoveTaskToList.Add(Enums.TaskStatus.Completed);

            completedList.CurrentSectionStatus = Enums.TaskStatus.Completed;

            AllSectionItems.Add(openList);
            AllSectionItems.Add(inProgressList);
            AllSectionItems.Add(resolvedList);
            AllSectionItems.Add(verifiedList);
            AllSectionItems.Add(completedList);

            #endregion Init sections

            _taskRepository = taskRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        #endregion Constructors

        public override async Task Initialize()
        {
            await base.Initialize();

            OrganizationName = SecureStorageHelper.GetString(Constants.OranizationName);

            List<TaskModelEntity> taskEntities = await _taskRepository.GetAllTasksList(SecureStorageHelper.GetString(Constants.CurrentUserId));

            var tasks = _mapper.Map<List<TaskModelEntity>, List<TaskModelResult>>(taskEntities);

            foreach (var section in AllSectionItems)
            {
                section.TaskItems = new ObservableCollection<TaskModelResult>(tasks.Where(x => x.TaskStatus == section.CurrentSectionStatus).ToList());
            }
        }
        #region Properties

        private string _organizationName;
        public string OrganizationName
        {
            get => _organizationName ?? string.Empty;
            set => SetProperty(ref _organizationName, value);
        }

        private ObservableCollection<SectionModelResult> _allSectionItems = new ObservableCollection<SectionModelResult>();
        public ObservableCollection<SectionModelResult> AllSectionItems
        {
            get => _allSectionItems;
            set => SetProperty(ref _allSectionItems, value);
        }

        #endregion Properties

        #region Commands  

        public IMvxAsyncCommand CreateTaskCommand => new MvxAsyncCommand(async () =>
        {
            var destructionResult = await NavigationService.Navigate<CreateTaskViewModel, TaskModelResult, DestructionResult<TaskModelResult>>(new TaskModelResult());
            if (destructionResult != null && destructionResult.Entity != null)
            {
                destructionResult.Entity.CreatorUserId = SecureStorageHelper.GetString(Constants.CurrentUserId);
                var taskEntity = _mapper.Map<TaskModelResult, TaskModelEntity>(destructionResult.Entity);
                await _taskRepository.Create(taskEntity);
                AllSectionItems.FirstOrDefault().TaskItems.Add(destructionResult.Entity);
            }
        });


        public IMvxAsyncCommand<TaskModelResult> ItemSelectedCommand => new MvxAsyncCommand<TaskModelResult>(async (model) =>
        {
            var destructionResult = await NavigationService.Navigate<EditTaskViewModel, TaskModelResult, DestructionResult<TaskModelResult>>(model);
            if (destructionResult == null || destructionResult.Entity == null)
            {
                return;
            }

            var taskEntity = _mapper.Map<TaskModelResult, TaskModelEntity>(destructionResult.Entity);

            if (destructionResult.Destroyed)
            {
                await _taskRepository.Delete(taskEntity);
                await _imageRepository.DeleteAllImages(taskEntity.Id);
                
                AllSectionItems.FirstOrDefault(x => x.TaskItems.Contains(model)).TaskItems.Remove(model);
                return;
            }


            await _taskRepository.Update(taskEntity);

        });

        #endregion Commands

        #region Methods

        public void ShowErrorMovedMessage(string from, string to)
        {
            UserDialogs.Alert($"You don't want move task from \"{from}\" to \"{to}\"", "Move error");
        }

        public async void UpdateTask(TaskModelResult task)
        {
            var taskEntity =  _mapper.Map<TaskModelResult, TaskModelEntity>(task);
            await _taskRepository.Update(taskEntity);
        }

        #endregion Methods
    }
}
