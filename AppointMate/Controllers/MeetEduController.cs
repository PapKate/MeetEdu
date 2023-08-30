using AppointMate.Helpers;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

namespace AppointMate
{

    /// <summary>
    /// Controller used for handing the requests related to an AppointMate related application
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
        public async Task<ActionResult<UserResponseModel>> LoginAsync([FromBody] LogInRequestModel model)
            => (await DI.AccountsRepository.LoginAsync(model)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Users

        /// <summary>
        /// Gets the user with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.UserRoute)]
        public async Task<ActionResult<UserResponseModel>?> GetUserAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Users, x => x.ToResponseModel(), x => x.Id.ToString() == id, cancellationToken);

        #endregion

        #region Universities

        /// <summary>
        /// Gets the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.UniversitiesRoute)]
        public async Task<ActionResult<UniversityResponseModel>?> GetUniversityAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Universities, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        #endregion

        #region Departments

        /// <summary>
        /// Gets the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.DepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>?> GetDepartmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Departments, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        #region Contact

        #endregion

        #endregion

        #region Professors

        /// <summary>
        /// Gets the professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetEduAPIRoutes.ProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>?> GetProfessorAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Professors, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

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
            => await ControllerHelpers.GetManyAsync(AppointMateDbMapper.AppointmentRules,
                                                    x => x.ToResponseModel(),
                                                    Builders<AppointmentRuleEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.DateCreated);

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
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Members, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

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
        [Route(MeetEduAPIRoutes.SavedProfessorsRoute)]
        public async Task<ActionResult<IEnumerable<DepartmentResponseModel>>> GetMemberSavedDepartmentsAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user savedProfessors department with the specified user memberId
            var savedDepartments = await AppointMateDbMapper.MemberSavedDepartments.SelectAsync(x => x.MemberId.ToString() == id);

            // If no savedProfessors department is found...
            if (savedDepartments is null)
                return NotFound();

            var departments = await AppointMateDbMapper.Departments.SelectAsync(x => savedDepartments.Any(y => y.DepartmentId == x.Id), cancellationToken);

            return new OkObjectResult(departments.Select(x => x.ToResponseModel()));
        }

        /// <summary>
        /// Gets the saved department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<DepartmentResponseModel>> GetMemberSavedDepartmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user saved department with the specified id
            var savedDepartment = await AppointMateDbMapper.MemberSavedDepartments.FirstOrDefaultAsync(x => x.Id.ToString() == id);

            // If the saved department is not found...
            if (savedDepartment is null)
                return NotFound();

            var department = await AppointMateDbMapper.Departments.FirstOrDefaultAsync(x => x.Id == savedDepartment.DepartmentId, cancellationToken);

            return department.ToResponseModel();
        }

        /// <summary>
        /// Deletes the saved department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
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
        public async Task<ActionResult<IEnumerable<ProfessorResponseModel>>> GetMemberSavedProfessorsAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user saved professor with the specified user memberId
            var savedProfessors = await AppointMateDbMapper.MemberSavedProfessors.SelectAsync(x => x.MemberId.ToString() == id);

            // If no saved professor is found...
            if (savedProfessors is null)
                return NotFound();

            var departments = await AppointMateDbMapper.Departments.SelectAsync(x => savedProfessors.Any(y => y.ProfessorId == x.Id), cancellationToken);

            return new OkObjectResult(departments.Select(x => x.ToResponseModel()));
        }

        /// <summary>
        /// Gets the saved professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ProfessorResponseModel>> GetMemberSavedProfessorAsync([FromRoute] string id, CancellationToken cancellationToken = default)
        {
            // Get the user saved professor with the specified memberId
            var savedProfessor = await AppointMateDbMapper.MemberSavedProfessors.FirstOrDefaultAsync(x => x.Id.ToString() == id);

            // If the saved professor is not found...
            if (savedProfessor is null)
                return NotFound();

            var professor = await AppointMateDbMapper.Professors.FirstOrDefaultAsync(x => x.Id == savedProfessor.ProfessorId, cancellationToken);

            return professor.ToResponseModel();
        }

        /// <summary>
        /// Deletes the saved professor with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The memberId</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
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
