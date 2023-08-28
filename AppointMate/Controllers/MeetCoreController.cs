using AppointMate.Helpers;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

namespace AppointMate
{
    /// <summary>
    /// Controller used for handing the requests related to a MeetCore related application
    /// </summary>
    public class MeetCoreController : Controller
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MeetCoreController() : base()
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
        [Route(MeetCoreAPIRoutes.RegisterRoute)]
        public async Task<ActionResult<UserResponseModel>> RegisterUserAsync([FromBody] UserRequestModel model)
            => (await DI.UsersRepository.AddUserAsync(model)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Validates the user credentials sent by the user and returns the user
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.LogInRoute)]
        public async Task<ActionResult<UserResponseModel>> LoginAsync([FromBody] LogInRequestModel model)
            => (await DI.AccountsRepository.LoginAsync(model)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Universities

        /// <summary>
        /// Gets the universities
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.UniversitiesRoute)]
        public async Task<ActionResult<IEnumerable<UniversityResponseModel>>> GetUniversitiesAsync([FromQuery] APIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(AppointMateDbMapper.Universities,
                                                    x => x.ToResponseModel(),
                                                    Builders<UniversityEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.Name);

        /// <summary>
        /// Creates a university
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.UniversitiesRoute)]
        public async Task<ActionResult<UniversityResponseModel>> AddUniversityAsync([FromBody] UniversityRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.AddUniversityAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.UniversityRoute)]
        public async Task<ActionResult<UniversityResponseModel>?> GetUniversityAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Universities, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.UniversityRoute)]
        public async Task<ActionResult<UniversityResponseModel>> UpdateUniversityAsync([FromRoute] string id, [FromBody] UniversityRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.UpdateUniversityAsync(id.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the university with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.UniversityRoute)]
        public async Task<ActionResult<UniversityResponseModel>> DeleteUniversityAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => (await DI.UniversitiesRepository.DeleteUniversityAsync(id.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #region Labels

        #endregion

        #endregion

        #region Departments

        /// <summary>
        /// Gets the departments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentsRoute)]
        public async Task<ActionResult<IEnumerable<DepartmentResponseModel>>> GetDepartmentsAsync([FromQuery] DepartmentAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(AppointMateDbMapper.Departments,
                                                    x => x.ToResponseModel(),
                                                    Builders<DepartmentEntity>.Filter.Empty, args,
                                                    cancellationToken, x => x.Name);

        /// <summary>
        /// Creates a department
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.DepartmentsRoute)]
        public async Task<ActionResult<DepartmentResponseModel>> AddDepartmentAsync([FromBody] DepartmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.AddDepartmentAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.DepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>?> GetDepartmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Departments, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.DepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>> UpdateDepartmentAsync([FromRoute] string id, [FromBody] DepartmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.UpdateDepartmentAsync(id.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the department with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.DepartmentRoute)]
        public async Task<ActionResult<DepartmentResponseModel>> DeleteDepartmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => (await DI.DepartmentsRepository.DeleteDepartmentAsync(id.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #region Layout 

        #endregion

        #endregion

        #region Secretaries

        /// <summary>
        /// Gets the secretaries
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.SecretariesRoute)]
        public async Task<ActionResult<IEnumerable<SecretaryResponseModel>>> GetSecretariesAsync([FromQuery] StafMemberAPIArgs args, CancellationToken cancellationToken = default) 
            => await ControllerHelpers.GetManyAsync(AppointMateDbMapper.Secretaries, 
                                                    x => x.ToResponseModel(), 
                                                    args.CreateFilters<SecretaryEntity>().AggregateFilters(), args, 
                                                    cancellationToken, x => x.Role);

        /// <summary>
        /// Creates a secretary
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.SecretariesRoute)]
        public async Task<ActionResult<SecretaryResponseModel>> AddSecretaryAsync([FromBody] SecretaryRequestModel model, CancellationToken cancellationToken = default) 
            => (await DI.SecretariesRepository.AddSecretaryAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the secretary
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.SecretaryRoute)]
        public async Task<ActionResult<SecretaryResponseModel>?> GetSecretaryAsync([FromRoute] string id, CancellationToken cancellationToken = default) 
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Secretaries, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.SecretaryRoute)]
        public async Task<ActionResult<SecretaryResponseModel>> UpdateSecretaryAsync([FromRoute] string id, [FromBody] SecretaryRequestModel model, [FromBody] UserRequestModel user, CancellationToken cancellationToken = default)
            => (await DI.SecretariesRepository.UpdateSecretaryAsync(id.ToObjectId(), model, user, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.SecretaryRoute)]
        public async Task<ActionResult<SecretaryResponseModel>> DeleteSecretaryAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => (await DI.SecretariesRepository.DeleteSecretaryAsync(id.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Professors

        /// <summary>
        /// Gets the professors
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.ProfessorsRoute)]
        public async Task<ActionResult<IEnumerable<ProfessorResponseModel>>> GetProfessorsAsync([FromQuery] StafMemberAPIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(AppointMateDbMapper.Professors,
                                                    x => x.ToResponseModel(),
                                                    args.CreateFilters<ProfessorEntity>().AggregateFilters(), args,
                                                    cancellationToken, x => x.Field);

        /// <summary>
        /// Creates a professor
        /// </summary>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPost]
        [Route(MeetCoreAPIRoutes.ProfessorsRoute)]
        public async Task<ActionResult<ProfessorResponseModel>> AddProfessorAsync([FromBody] ProfessorRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.ProfessorsRepository.AddProfessorAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the secretary
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.ProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>?> GetProfessorAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Professors, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.ProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>> UpdateProfessorAsync([FromRoute] string id, [FromBody] ProfessorRequestModel model, [FromBody] UserRequestModel user, CancellationToken cancellationToken = default)
            => (await DI.ProfessorsRepository.UpdateProfessorAsync(id.ToObjectId(), model, user, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the secretary with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.ProfessorRoute)]
        public async Task<ActionResult<ProfessorResponseModel>> DeleteProfessorAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => (await DI.ProfessorsRepository.DeleteProfessorAsync(id.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #region Appointment Rules

        #endregion

        #region Appointments

        /// <summary>
        /// Gets the appointments
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.AppointmentsRoute)]
        public async Task<ActionResult<IEnumerable<AppointmentResponseModel>>> GetAppointmentsAsync([FromQuery] APIArgs args, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetManyAsync(AppointMateDbMapper.Appointments,
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
        [Route(MeetCoreAPIRoutes.AppointmentsRoute)]
        public async Task<ActionResult<AppointmentResponseModel>> AddAppointmentAsync([FromBody] AppointmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.AddAppointmentAsync(model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Gets the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpGet]
        [Route(MeetCoreAPIRoutes.AppointmentRoute)]
        public async Task<ActionResult<AppointmentResponseModel>?> GetAppointmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => await ControllerHelpers.GetAsync(AppointMateDbMapper.Appointments, x => x.ToResponseModel(), x => x.Id == id.ToObjectId(), cancellationToken);

        /// <summary>
        /// Updates the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="model">The model</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpPut]
        [Route(MeetCoreAPIRoutes.UniversityRoute)]
        public async Task<ActionResult<AppointmentResponseModel>> UpdateAppointmentAsync([FromRoute] string id, [FromBody] AppointmentRequestModel model, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.UpdateAppointmentAsync(id.ToObjectId(), model, cancellationToken)).ToActionResult(x => x.ToResponseModel());

        /// <summary>
        /// Deletes the appointment with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(MeetCoreAPIRoutes.AppointmentRoute)]
        public async Task<ActionResult<AppointmentResponseModel>> DeleteAppointmentAsync([FromRoute] string id, CancellationToken cancellationToken = default)
            => (await DI.AppointmentsRepository.DeleteAppointmentAsync(id.ToObjectId(), cancellationToken)).ToActionResult(x => x.ToResponseModel());

        #endregion

        #endregion
    }
}
