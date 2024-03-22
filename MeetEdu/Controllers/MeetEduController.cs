using MeetEdu.Helpers;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

namespace MeetEdu
{

    /// <summary>
    /// Controller used for handing the requests related to an MeetEdu related application
    /// </summary>
    public class MeetEduController : Controller
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MeetEduController() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Register / Login

        /// <summary>
        /// Registers a user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetEduAPIRoutes.RegisterRoute)]
        public async Task<ActionResult<UserResponseModel>> RegisterUserAsync([FromBody] UserRequestModel model)
            => (await DI.UsersRepository.AddUserAsync(model)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Validates the user credentials sent by the user and returns the user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetEduAPIRoutes.LogInRoute)]
        public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LogInRequestModel model)
            => (await DI.AccountsRepository.LoginAsync(model)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Users

        /// <summary>
        /// Gets the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.UserRoute)]
        public async Task<ActionResult<UserResponseModel>?> GetUserAsync([FromRoute] string userId, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Users, x => x.ToResponseModel(), x => x.Id.ToString() == userId, cancellationToken);

        #endregion

        #region Universities

        /// <summary>
        /// Gets the universities
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.UniversitiesRoute)]
        public async Task<ActionResult<IEnumerable<UniversityResponseModel>>> GetUniversitiesAsync([FromQuery] APIArgs? args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Universities,
                                                    x => x.ToResponseModel(),
                                                    Builders<UniversityEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.Name);

        /// <summary>
        /// Gets the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.UniversityRoute)]
        public async Task<ActionResult<UniversityResponseModel>?> GetUniversityAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Universities, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        #endregion

        #region Departments

        /// <summary>
        /// Gets the departments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.DepartmentsRoute)]
        public async Task<ActionResult<IEnumerable<DepartmentResponseModel>>> GetDepartmentsAsync([FromQuery] DepartmentAPIArgs? args = null, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Departments,
                                                    x => x.ToResponseModel(),
                                                    args?.CreateFilters()?.AggregateFilters() ?? null, args,
                                                    cancellationToken, x => x.Name);

        /// <summary>
        /// Gets the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.DepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>?> GetDepartmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Departments, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        #region Contact

        /// <summary>
        /// Gets the department contact messages
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.DepartmentContactMessagesRoute)]
        public async Task<ActionResult<IEnumerable<DepartmentContactMessageResponseModel>>> GetDepartmentContactMessagesAsync([FromQuery] DepartmentRelatedAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.DepartmentContactMessages,
                                                    x => x.ToResponseModel(),
                                                    args.CreateFilters<DepartmentContactMessageEntity>().AggregateFilters(), args,
                                                    cancellationToken, x => x.DepartmentId);

        /// <summary>
        /// Creates a department contact message
        /// </summary>
        /// <param name="departmentId">The department id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetEduAPIRoutes.DepartmentContactMessagesRoute)]
        public async Task<ActionResult<DepartmentContactMessageResponseModel>> AddDepartmentContactMessageAsync([FromRoute] string departmentId, [FromBody] DepartmentContactMessageRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.AddDepartmentContactMessageAsync(departmentId.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the department contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.DepartmentContactMessageRoute)]
        public async Task<ActionResult<DepartmentContactMessageResponseModel>?> GetDepartmentContactMessageAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.DepartmentContactMessages, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the department contact message with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetEduAPIRoutes.DepartmentContactMessageRoute)]
        public async Task<ActionResult<DepartmentContactMessageResponseModel>> UpdateDepartmentContactMessageAsync([FromRoute] string id, [FromBody] DepartmentContactMessageRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.UpdateDepartmentContactMessageAsync(id.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #endregion

        #region Professors

        /// <summary>
        /// Gets the professors
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.ProfessorsRoute)]
        public async Task<ActionResult<IEnumerable<ProfessorResponseModel>>> GetProfessorsAsync([FromQuery] DepartmentRelatedAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Professors,
                                                    x => x.ToResponseModel(),
                                                    args.CreateFilters<ProfessorEntity>().AggregateFilters(), args,
                                                    cancellationToken, x => x.Field);

        /// <summary>
        /// Gets the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.ProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>?> GetProfessorAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Professors, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        #endregion

        #region Appointment Rules

        /// <summary>
        /// Gets the appointment rules
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.AppointmentRulesRoute)]
        public async Task<ActionResult<IEnumerable<AppointmentRuleResponseModel>>> GetAppointmentRulesAsync([FromQuery] AppointmentRuleAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.AppointmentRules,
                                                    x => x.ToResponseModel(),
                                                    Builders<AppointmentRuleEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.DateCreated);

        #endregion

        #region Appointments

        /// <summary>
        /// Gets the appointments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.AppointmentsRoute)]
        public async Task<ActionResult<IEnumerable<AppointmentResponseModel>>> GetAppointmentsAsync([FromQuery] APIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(MeetEduDbMapper.Appointments,
                                                    x => x.ToResponseModel(),
                                                    Builders<AppointmentEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.DateCreated);

        /// <summary>
        /// Creates a appointment
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetEduAPIRoutes.AppointmentsRoute)]
        public async Task<ActionResult<AppointmentResponseModel>> AddAppointmentAsync([FromBody] AppointmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.AddAppointmentAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.AppointmentRoute)]
        public async Task<ActionResult<AppointmentResponseModel>?> GetAppointmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Appointments, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetEduAPIRoutes.AppointmentRoute)]
        public async Task<ActionResult<AppointmentResponseModel>> UpdateAppointmentAsync([FromRoute] string id, [FromBody] AppointmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.UpdateAppointmentAsync(id.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Members

        /// <summary>
        /// Gets the member with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.MemberRoute)]
        public async Task<ActionResult<MemberResponseModel>?> GetMemberAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(MeetEduDbMapper.Members, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        #region Saved Departments

        /// <summary>
        /// Adds a saved the department with the specified <paramref name="departmentId"/> to the member with the specified <paramref name="memberId"/>
        /// </summary>
        /// <param name="memberId">The memberId</param>
        /// <param name="departmentId">The department id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetEduAPIRoutes.SavedDepartmentsRoute)]
        public async Task<ActionResult<bool>> AddMemberSavedDepartmentAsync([FromBody] string memberId, [FromBody] string departmentId, CancellationToken cancellationToken = default)
        {
            var entity = await DI.MembersRepository.AddMemberSavedDpartmentAsync(memberId.ToObjectId(), departmentId.ToObjectId());

            return entity.ToActionResult(x => true);
        }

        /// <summary>
        /// Gets the saved departments of the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The user id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.SavedDepartmentsRoute)]
        public async Task<ActionResult<IEnumerable<DepartmentResponseModel>>> GetMemberSavedDepartmentsAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user savedProfessors department with the specified user memberId
            var savedDepartments = await MeetEduDbMapper.MemberSavedDepartments.SelectAsync(x => x.MemberId.ToString() == id);

            // If no savedProfessors department is found...
            if (savedDepartments is null)
                return NotFound();

            var departments = await MeetEduDbMapper.Departments.SelectAsync(x => savedDepartments.Any(y => y.DepartmentId == x.Id), cancellationToken);

            return new OkObjectResult(departments.Select(x => x.ToResponseModel()));
        }

        /// <summary>
        /// Gets the saved department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.SavedDepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>> GetMemberSavedDepartmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user saved department with the specified id
            var savedDepartment = await MeetEduDbMapper.MemberSavedDepartments.FirstOrDefaultAsync(x => x.Id.ToString() == id);

            // If the saved department is not found...
            if (savedDepartment is null)
                return NotFound();

            var department = await MeetEduDbMapper.Departments.FirstOrDefaultAsync(x => x.Id == savedDepartment.DepartmentId, cancellationToken);

            return department.ToResponseModel();
        }

        /// <summary>
        /// Deletes the saved department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetEduAPIRoutes.SavedDepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>> DeleteSavedDeparmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            var response = await DI.MembersRepository.DeleteMemberSavedDepartmentAsync(id.ToObjectId());

            if (!response.IsSuccessful || response.Result is null)
                return StatusCode(response.StatusCode ?? 400, response);

            var department = await GetDepartmentAsync(response.Result.DepartmentId.ToString(), cancellationToken);

            return department!;
        }

        #endregion

        #region Saved Professors

        /// <summary>
        /// Adds a saved the professor with the specified <paramref name="professorId"/> to the member with the specified <paramref name="memberId"/>
        /// </summary>
        /// <param name="memberId">The memberId</param>
        /// <param name="professorId">The professor </param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetEduAPIRoutes.SavedProfessorsRoute)]
        public async Task<ActionResult<bool>> AddMemberSavedProfessorAsync([FromRoute] string memberId, [FromBody] string professorId, CancellationToken cancellationToken = default)
        {
            var entity = await DI.MembersRepository.AddMemberSavedProfessorAsync(memberId.ToObjectId(), professorId.ToObjectId());

            return entity.ToActionResult(x => true);
        }

        /// <summary>
        /// Gets the saved professors of the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The user memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.SavedProfessorsRoute)]
        public async Task<ActionResult<IEnumerable<ProfessorResponseModel>>> GetMemberSavedProfessorsAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user saved professor with the specified user memberId
            var savedProfessors = await MeetEduDbMapper.MemberSavedProfessors.SelectAsync(x => x.MemberId.ToString() == id);

            // If no saved professor is found...
            if (savedProfessors is null)
                return NotFound();

            var departments = await MeetEduDbMapper.Departments.SelectAsync(x => savedProfessors.Any(y => y.ProfessorId == x.Id), cancellationToken);

            return new OkObjectResult(departments.Select(x => x.ToResponseModel()));
        }

        /// <summary>
        /// Gets the saved professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.SavedProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>> GetMemberSavedProfessorAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user saved professor with the specified memberId
            var savedProfessor = await MeetEduDbMapper.MemberSavedProfessors.FirstOrDefaultAsync(x => x.Id.ToString() == id);

            // If the saved professor is not found...
            if (savedProfessor is null)
                return NotFound();

            var professor = await MeetEduDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == savedProfessor.ProfessorId, cancellationToken);

            return professor.ToResponseModel();
        }

        /// <summary>
        /// Deletes the saved professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetEduAPIRoutes.SavedProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>> DeleteSavedProfessorAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            var response = await DI.MembersRepository.DeleteMemberFavoriteProfessorAsync(id.ToObjectId());

            if (!response.IsSuccessful || response.Result is null)
                return StatusCode(response.StatusCode ?? 400, response);

            var professor = await GetProfessorAsync(response.Result.ProfessorId.ToString(), cancellationToken);

            return professor!;
        }

        #endregion

        #endregion

        #endregion
    }
}
