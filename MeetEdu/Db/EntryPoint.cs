using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace MeetEdu
{
    /// <summary>
    /// Contains methods for filling the DB with data
    /// </summary>
    public class EntryPoint
    {
        #region Public Properties

        /// <summary>
        /// The universities
        /// </summary>
        public static List<UniversityResponseModel> Universities = new();

        /// <summary>
        /// The university of Patras departments
        /// </summary>
        public static List<DepartmentResponseModel> PaPaDepartments = new();

        #endregion

        #region Public Properties

        /// <summary>
        /// The controller
        /// </summary>
        public MeetCoreController Controller { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EntryPoint(MeetCoreController controller) : base()
        {
            Controller = controller;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds labels to the DB
        /// </summary>
        /// <returns></returns>
        public async Task AddUniversityLabelsAsync()
        {
            var labels = new List<LabelRequestModel>()
            { 
            
            };

            foreach (var label in labels)
            {
                await Controller.AddUniversityLabelAsync(label);
            }
        }

        /// <summary>
        /// Adds universities to the DB
        /// </summary>
        /// <returns></returns>
        public async Task AddUniversitiesAsync()
        {
            var universityRequests = new List<UniversityRequestModel>()
            { 
                new()
                {
                    Name = "University of Patras",
                    Color = "EBCA97",
                    ImageUrl = new Uri("https://localhost:7125/universities/651a674479477d03820659bd-21roaglb.png"),
                    LabelIds = new List<string>() 
                    {
                    }
                },
                new()
                {
                    Name = "University of Crete",
                    Color = "D8726A",
                    ImageUrl = new Uri("https://localhost:7125/universities/65ede25ebc5eba24b98e3200-agr5bpoe.png"),
                    LabelIds = new List<string>()
                    {
                    }
                },
                new()
                {
                    Name = "National Technical University of Athens",
                    Color = "C5C5C5",
                    ImageUrl = new Uri("https://localhost:7125/universities/65ede26abc5eba24b98e3201-peaoh1jd.png"),
                    LabelIds = new List<string>()
                    {
                    }
                },
                new()
                {
                    Name = "University of Mecedonia",
                    Color = "0067C8",
                    ImageUrl = new Uri("https://localhost:7125/universities/65ede276bc5eba24b98e3202-0f2tuas3.png"),
                    LabelIds = new List<string>()
                    {
                    }
                },
                new()
                {
                    Name = "Aristotle University Of Thessaloniki",
                    Color = "952927",
                    ImageUrl = new Uri("https://localhost:7125/universities/65ede280bc5eba24b98e3203-ukik44qp.png"),
                    LabelIds = new List<string>()
                    {
                    }
                },
                new()
                {
                    Name = "National and Kapodistrian University of Athens",
                    Color = "646464",
                    ImageUrl = new Uri("https://localhost:7125/universities/65ede2e0bc5eba24b98e320d-npqjqpmr.png"),
                    LabelIds = new List<string>()
                    {
                    }
                },
                new()
                {
                    Name = "University Of Thessaly",
                    Color = "A1DAFB",
                    ImageUrl = new Uri("https://localhost:7125/universities/65ede2efbc5eba24b98e320e-vorvfk5r.png"),
                    LabelIds = new List<string>()
                    {
                    }
                },
                new()
                {
                    Name = "Hellenic Mediterranean University",
                    Color = "B69A56",
                    ImageUrl = new Uri("https://localhost:7125/universities/65ede2fdbc5eba24b98e320f-h5cd55ml.png"),
                    LabelIds = new List<string>()
                    {
                    }
                }
            };

            foreach (var university in universityRequests)
            {
                var result = await Controller.AddUniversityAsync(university);
                if (result.Result is not null && result.Result is ObjectResult uniResult && uniResult.Value is not null)
                    Universities.Add((UniversityResponseModel)uniResult.Value);
            }
        }

        /// <summary>
        /// Adds departments to the DB
        /// </summary>
        /// <returns></returns>
        public async Task AddDepartmentsAsync()
        {
            #region Papa

            var papaDepartmentRequests = new List<DepartmentRequestModel>()
            {
                new()
                {
                    UniversityId = Universities[0].Id,
                    Name = "Computer Engineering & Informatics Department",
                    Color = "49B6FF",
                    Email = "secretary@ceid.upatras.com",
                    Category = DepartmentType.Engineering,
                    PhoneNumber = new PhoneNumber(30, "2610623555"),
                    Location = new Location()
                    {
                        Country = CountryCode.GR,
                        State = "Achaia",
                        City = "Patras",
                        Postcode = "26441",
                        Address = "Politechniou 23",
                        Longitude = 21.795411784348772,
                        Latitude = 38.289857021650526
                    },
                    ImageUrl = new Uri(""),
                    Quote = "The future belongs to those who believe in the beauty of their dreams",
                    Description = "The C.E.I. department was founded in 1979 and has been operational since 1980. It is the pioneering department in the field of Computer Technology, Informatics, and Communications in Greece. It ranks among the top University Departments in Greece and has received significant international recognition. Its purpose and mission are teaching and research in the science and technology of computers and the study of their applications. Studies at the Department require active, consistent, and creative participation of students in the educational activities of the Curriculum, elements necessary for the successful completion of their studies, but the final result justifies the expectations of the students. The department is organized into three sectors:",
                    Note = "The department has also created or participated in collaboration with other university departments in several specialized postgraduate programs, which cover all cutting-edge subjects in science and technology.",
                    LayoutDescription = "",
                    SecretaryDescription = "The Secretariat is located on the first floor of the New Building (Kazantzaki Street) of the Department of Computer Engineering and Informatics of the University of Patras. It receives students of the Department on Monday, Wednesday, and Friday from 11:30 am to 1:30 pm.",
                    WorkHours = new()
                    {
                        Name = "Secretary hours",
                        Color = "FF499E",
                        Note = "Closed Wednesday & Friday afternoons.",
                        WeeklyHours = new List<DayOfWeekTimeRange>()
                        {
                            new(string.Empty, DayOfWeek.Monday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(16, 00), new TimeOnly(17, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Friday, new TimeOnly(10, 00), new TimeOnly(14, 00)),
                        }
                    },
                    ContactMessageTemplate = new()
                    {
                        Description = "Submit electronically any questions or requests you may have via the following form, and we will assist you promptly. We encourage your communication with us as we continuously strive to provide upgraded services to our customers. By completing this form, you help us improve the services we provide to you.",
                        Note = "For more information, contact us, we are available for any questions you may have."
                    },
                    Fields = new List<string>()
                    {
                        "Field of Applications and Foundations of Computer Science",
                        "Field of Computer Logic",
                        "Field of Computer Hardware and Architecture"
                    }
                },
                new()
                {
                    UniversityId = Universities[0].Id,
                    Name = "Electrical & Computer Engineering Department",
                    Color = "EEC643",
                    Email = "ecesecr@upatras.gr",
                    Category = DepartmentType.Engineering,
                    PhoneNumber = new PhoneNumber(30, "2610996492"),
                    Location = new Location()
                    {
                        Country = CountryCode.GR,
                        State = "Achaia",
                        City = "Patras",
                        Postcode = "26441",
                        Address = "Politechniou 2",
                        Longitude = 21.795411784348772,
                        Latitude = 38.289857021650526
                    },
                    ImageUrl = new Uri(""),
                    Description = "Fifty seven years have passed since the foundation of the Electrical Engineering (EE) Department (25.9.1967) as the first engineering department whereby the Polytechnic School of the University of Patras was founded and operated. The initial minimal infrastructure and the small number of teaching and research staff as well as the small number of administrative staff and personel were not obstacles for the fast development of the EE Department and its great contribution to the creation of significant scientific manpower in Greece, very useful for our country and with international recognition. The broadening of  scientific subjects led to the extens ion of the title of the Electrical Engineering Department (EE) to Electrical and Computer Engineering Department (ECE) in 1995. he undergraduate program of studies is divided into 10 semesters. The first six semesters are comprised of 40 obligatory courses common to all students, 2 selective courses of general education and 4 courses of foreign language and terminology. In the first four semesters, courses of general background are taught (e.g. Matchematics and Physics), while the core courses of the Electrical and Computer Engineer are offered in the 5th and 6th semester. At the beginning of the 7th semester, the students have to specialize their studies, by choosing one of the following divisions (fields of specialization):",
                    Note = "The courses offered within a specialization field are mainly special technological courses. During the last four semesters (7 - 10), students are obliged to attend 6 (out of 22) courses, selected from at least two other fields of specialization. In this way, their background knowledge is expanded, while a good degree of specialization is achieved.",
                    LayoutDescription = "",
                    SecretaryDescription = "Electrical & Computer Engineering Building 1st floor. Students should apply to the Secretariat of the Department of Electrical & Computer Engineering for information and help concerning the following matters:\r\n\r\n* Information on registration following their admission to the Department, and on all other matters pertaining to their student status.\r\n* Applications for registration, renewal of registration, re registration, choice of electives, etc.\r\n* Requests for copies of student records and degrees.\r\n* Applications for scholarships and loans. \" Requests for student ID cards and student tickets.\r\n* Information on any other matter concerning the Department.",
                    WorkHours = new()
                    {
                        Name = "Secretary hours",
                        Color = "0D21A1",
                        Note = "Closed Wednesday & Friday afternoons.",
                        WeeklyHours = new List<DayOfWeekTimeRange>()
                        {
                            new(string.Empty, DayOfWeek.Monday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(16, 00), new TimeOnly(17, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Friday, new TimeOnly(10, 00), new TimeOnly(14, 00)),
                        }
                    },
                    ContactMessageTemplate = new()
                    {
                        Description = "Submit electronically any questions or requests you may have via the following form, and we will assist you promptly. We encourage your communication with us as we continuously strive to provide upgraded services to our customers. By completing this form, you help us improve the services we provide to you.",
                        Note = "For more information, contact us, we are available for any questions you may have."
                    },
                    Fields = new List<string>()
                    {
                        "Division of Telecommunications and Information Technology",
                        "Division of Electrical Power Systems",
                        "Division of Systems and Automatic Control",
                        "Division of Electronics and Computers"
                    }
                },
                new()
                {
                    UniversityId = Universities[0].Id,
                    Name = "Mechanical Engineering and Aeronautics Department",
                    Color = "DD1C1A",
                    Email = "secretar@mech.upatras.gr",
                    Category = DepartmentType.Engineering,
                    PhoneNumber = new PhoneNumber(30, "2610969400"),
                    Location = new Location()
                    {
                        Country = CountryCode.GR,
                        State = "Achaia",
                        City = "Patras",
                        Postcode = "26441",
                        Address = "Politechniou 2",
                        Longitude = 21.795411784348772,
                        Latitude = 38.289857021650526
                    },
                    ImageUrl = new Uri(""),
                    Description = "The Department of Mechanical Engineering and Aeronautics offers a broad-based education for engineering, encompassing professional disciplines associated with current and future needs as well as with developments in both Mechanical and Aeronautical Engineering. It was founded in 1967, providing curricula in Mechanical Engineering since 1972, on a five-year course basis and preparing graduates for industry, government, education and research both at home and abroad. Since 1995 the Department has been expanded and started offering curricula in Aeronautics in both undergraduate and post-graduate levels. The Department covers a variety of career objectives sought by its graduates, leading to a Diploma degree, equivalent to the MS of Engineering, after a five- year study and the successful accomplishment of a Diploma Thesis, equivalent to Masters Thesis.",
                    Note = "The division offers courses on: Mechanical Drawing – Theory of Machines and Mechanisms – Machine Elements – Engineering Design – Theory and Applications of CAD – Design and Planning of Production Systems – Intelligent Systems in Design and Manufacturing – Rapid Prototyping – Machine Tools – Material Processing – Metrology and Measurements – Maintenance of Machinery –  Machine Fault Diagnosis and Reliability- Automatic Control – Modeling, Identification and Optimization of Mechanical Systems – Mechatronic Systems – Robotics – Industrial Automatic Control – Applications of Artificial and Computational Intelligence – Stochastic Dynamic Signals and Systems – Acoustics – Sound Pollution – Medical Systems – Man-machine Systems",
                    LayoutDescription = "",
                    SecretaryDescription = "Students should apply to the Secretariat of the Department of Electrical & Computer Engineering for information and help concerning the following matters:\r\n\r\n* Information on registration following their admission to the Department, and on all other matters pertaining to their student status.\r\n* Applications for registration, renewal of registration, re registration, choice of electives, etc.\r\n* Requests for copies of student records and degrees.\r\n* Applications for scholarships and loans. \" Requests for student ID cards and student tickets.\r\n* Information on any other matter concerning the Department.",
                    WorkHours = new()
                    {
                        Name = "Secretary hours",
                        Color = "06AED5",
                        Note = "Closed Wednesday & Friday afternoons.",
                        WeeklyHours = new List<DayOfWeekTimeRange>()
                        {
                            new(string.Empty, DayOfWeek.Monday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(16, 00), new TimeOnly(17, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Friday, new TimeOnly(10, 00), new TimeOnly(14, 00)),
                        }
                    },
                    ContactMessageTemplate = new()
                    {
                        Description = "Submit electronically any questions or requests you may have via the following form, and we will assist you promptly. We encourage your communication with us as we continuously strive to provide upgraded services to our customers. By completing this form, you help us improve the services we provide to you.",
                        Note = "For more information, contact us, we are available for any questions you may have."
                    },
                    Fields = new List<string>()
                    {
                        "Division of Applied Mechanics, Technology of Materials and Biomechanics",
                        "Division of Energy, Aeronautics & Enviroment",
                        "Division of Management & Organization Studies"
                    }
                },
                new()
                {
                    UniversityId = Universities[0].Id,
                    Name = "Architecture Department",
                    Color = "9E4770",
                    Email = "archisec@upatras.gr",
                    Category = DepartmentType.Engineering,
                    PhoneNumber = new PhoneNumber(30, "2610997553"),
                    Location = new Location()
                    {
                        Country = CountryCode.GR,
                        State = "Achaia",
                        City = "Patras",
                        Postcode = "26441",
                        Address = "Politechniou 2",
                        Longitude = 21.795411784348772,
                        Latitude = 38.289857021650526
                    },
                    ImageUrl = new Uri(""),
                    Description = "The Department of Architecture celebrates its 40 years of operation this year. Organically integrated into the large academic institution of AUTh, it has adopted a progressive character, always remaining oriented towards extroversion and adopting a pluralistic approach to architectural design. The curriculum of the Department has from the outset, and continues to cover, all scales of architectural design, from the scale of the region, the city, and the landscape, to that of the building and the interior space, industrial design, construction, maintenance, and reuse of buildings and complexes. Visually, the theory and history of architecture and art frame the design studios, fostering critical thinking and creativity, placing architectural composition within the historical, social, cultural, and environmental framework that shapes it.",
                    Note = "The department's facilities include a well-organized and equipped digital design and fabrication laboratory that can support the investigative and innovative experimentation of students in architectural design studios. Additionally, there is a significant specialized architectural library that is integrated into the library network.",
                    LayoutDescription = "",
                    SecretaryDescription = "Students should apply to the Secretariat of the Department of Electrical & Computer Engineering for information and help concerning the following matters:\r\n\r\n* Information on registration following their admission to the Department, and on all other matters pertaining to their student status.\r\n* Applications for registration, renewal of registration, re registration, choice of electives, etc.\r\n* Requests for copies of student records and degrees.\r\n* Applications for scholarships and loans. \" Requests for student ID cards and student tickets.\r\n* Information on any other matter concerning the Department.",
                    WorkHours = new()
                    {
                        Name = "Secretary hours",
                        Color = "2E2532",
                        Note = "Closed Wednesday & Friday afternoons.",
                        WeeklyHours = new List<DayOfWeekTimeRange>()
                        {
                            new(string.Empty, DayOfWeek.Monday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(16, 00), new TimeOnly(17, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Friday, new TimeOnly(10, 00), new TimeOnly(14, 00)),
                        }
                    },
                    ContactMessageTemplate = new()
                    {
                        Description = "Submit electronically any questions or requests you may have via the following form, and we will assist you promptly. We encourage your communication with us as we continuously strive to provide upgraded services to our customers. By completing this form, you help us improve the services we provide to you.",
                        Note = "For more information, contact us, we are available for any questions you may have."
                    },
                    Fields = new List<string>()
                    {
                        "Division of Architectural Design and Visual Arts",
                        "Division of Architectural and Urban Planning",
                        "Division of Urban Planning, Spatial Planning, and Regional Development",
                        "Division of History of Architecture, Art History, Architectural Morphology, and Restoration",
                        "Division of Architectural Design and Architectural Technology"
                    }
                },
                new()
                {
                    UniversityId = Universities[0].Id,
                    Name = "Civil Engineering Department",
                    Color = "6EEB83",
                    Email = "civil@upatras.gr",
                    Category = DepartmentType.Engineering,
                    PhoneNumber = new PhoneNumber(30, "2610997553"),
                    Location = new Location()
                    {
                        Country = CountryCode.GR,
                        State = "Achaia",
                        City = "Patras",
                        Postcode = "26441",
                        Address = "Politechniou 2",
                        Longitude = 21.795411784348772,
                        Latitude = 38.289857021650526
                    },
                    ImageUrl = new Uri(""),
                    Description = "The Department of Civil Engineering was founded in 1972. It is located in a building with a gross surface area of more than 16000 m2 including classrooms, an auditorium, a drafting room, seminar rooms, a library room, a computer facility, staff offices, administrative areas, and laboratories of a gross area of about 5000 m2. The staff of the Department consists of 21 faculty members, 14 Professors Emeriti, 2 teaching associates, 5 technical associates, and 6 administrative members.\r\n\r\nThe Department consists of three Divisions, eight Laboratories, a Seismic Simulator facility, and two Computer Centers. The staff and the various functions of the Department, with the exception of the Computer Centers and the Seismic Simulator, are integrated under the three Divisions. The Department’s Divisions cover the areas of Structural Engineering, Geotechnical Engineering and Hydraulic Engineering, and Environmental Engineering and Transportation Engineering.\r\n\r\nThe Department operates 8 Laboratories for teaching and research purposes. These are the Structures, the Structural Materials, the Geotechnical Engineering, the Hydraulic Engineering, the Environmental Engineering, the Transportation and Ambient Mobility Systems, and the Construction, Infrastructure and City Management Laboratories. In addition, the Department has a Computer Center with a large number of personal computers, which provides adequate computing facilities primarily for undergraduate education. Computational facilities for research purposes are attached to each of the eight Laboratories of the Department.\r\n\r\nThe Department is also responsible for graduate education leading to the degrees of Postgraduate Diploma in the “Design of Resilient, Sustainable and Smart Infrastructures”, and Doctorate (PhD) in Civil Engineering through a comprehensive graduate studies program involving graduate level courses.",
                    Note = "The department's facilities include a well-organized and equipped digital design and fabrication laboratory that can support the investigative and innovative experimentation of students in architectural design studios. Additionally, there is a significant specialized architectural library that is integrated into the library network.",
                    LayoutDescription = "",
                    SecretaryDescription = "Students should apply to the Secretariat of the Department of Electrical & Computer Engineering for information and help concerning the following matters:\r\n\r\n* Information on registration following their admission to the Department, and on all other matters pertaining to their student status.\r\n* Applications for registration, renewal of registration, re registration, choice of electives, etc.\r\n* Requests for copies of student records and degrees.\r\n* Applications for scholarships and loans. \" Requests for student ID cards and student tickets.\r\n* Information on any other matter concerning the Department.",
                    WorkHours = new()
                    {
                        Name = "Secretary hours",
                        Color = "FF6B6B",
                        Note = "Closed Wednesday & Friday afternoons.",
                        WeeklyHours = new List<DayOfWeekTimeRange>()
                        {
                            new(string.Empty, DayOfWeek.Monday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(16, 00), new TimeOnly(17, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Friday, new TimeOnly(10, 00), new TimeOnly(14, 00)),
                        }
                    },
                    ContactMessageTemplate = new()
                    {
                        Description = "Submit electronically any questions or requests you may have via the following form, and we will assist you promptly. We encourage your communication with us as we continuously strive to provide upgraded services to our customers. By completing this form, you help us improve the services we provide to you.",
                        Note = "For more information, contact us, we are available for any questions you may have."
                    },
                    Fields = new List<string>()
                    {
                        "Division of Structural Engineering",
                        "Division of Geotechnical Engineering and Hydraulic Engineering",
                        "Division of Environmental Engineering and Transportation"
                    }
                },
                new()
                {
                    UniversityId = Universities[0].Id,
                    Name = "Chemical Engineering Department",
                    Color = "9448BC",
                    Email = "chemengsecr@upatras.gr",
                    Category = DepartmentType.Engineering,
                    PhoneNumber = new PhoneNumber(30, "2610969500"),
                    Location = new Location()
                    {
                        Country = CountryCode.GR,
                        State = "Achaia",
                        City = "Patras",
                        Postcode = "26441",
                        Address = "Politechniou 2",
                        Longitude = 21.795411784348772,
                        Latitude = 38.289857021650526
                    },
                    ImageUrl = new Uri(""),
                    Description = "The Department of Chemical Engineering (DCE) of the Engineering  School of the University of Patras was established in 1977. The mission of DCE is to produce chemical engineers educated in research, development and optimization of methods for production of industrial products, in materials technology, in environmental protection and in energy production.\r\n \r\nDCE follows the modern trends and international dynamics of the science of chemical engineering, which pioneers in areas such as biotechnology and biological engineering, nanotechnology and renewable and alternative energy forms, being a center of excellence in several of these areas.\r\n\r\nEducation and research in DCE are carried out according to international quality standards and have resulted in numerous distinctions of the Department, faculty and alumni who have proven able to meet successfully in the highly competitive Greek, European and international environment.\r\n\r\nFaculty and staff members in DCE are involved in major research projects funded by the European Union, the Greek General Secretariat for Research and Technology (GSRT), other Greek organizations and industry, in collaboration with some of the top universities and research centers globally.\r\n \r\nThe Department of Chemical Engineering is housed in two modern buildings located at the University of Patras Campus, with magnificent views of the mountains of Peloponnese and the Gulf of Patras.",
                    Note = "The department's facilities include a well-organized and equipped digital design and fabrication laboratory that can support the investigative and innovative experimentation of students in architectural design studios. Additionally, there is a significant specialized architectural library that is integrated into the library network.",
                    LayoutDescription = "",
                    SecretaryDescription = "Students should apply to the Secretariat of the Department of Electrical & Computer Engineering for information and help concerning the following matters:\r\n\r\n* Information on registration following their admission to the Department, and on all other matters pertaining to their student status.\r\n* Applications for registration, renewal of registration, re registration, choice of electives, etc.\r\n* Requests for copies of student records and degrees.\r\n* Applications for scholarships and loans. \" Requests for student ID cards and student tickets.\r\n* Information on any other matter concerning the Department.",
                    WorkHours = new()
                    {
                        Name = "Secretary hours",
                        Color = "480355",
                        Note = "Closed Wednesday & Friday afternoons.",
                        WeeklyHours = new List<DayOfWeekTimeRange>()
                        {
                            new(string.Empty, DayOfWeek.Monday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(12, 00), new TimeOnly(14, 00)),
                            new(string.Empty, DayOfWeek.Tuesday, new TimeOnly(16, 00), new TimeOnly(17, 00)),
                            new(string.Empty, DayOfWeek.Friday, new TimeOnly(10, 00), new TimeOnly(14, 00)),
                        }
                    },
                    ContactMessageTemplate = new()
                    {
                        Description = "Submit electronically any questions or requests you may have via the following form, and we will assist you promptly. We encourage your communication with us as we continuously strive to provide upgraded services to our customers. By completing this form, you help us improve the services we provide to you.",
                        Note = "For more information, contact us, we are available for any questions you may have."
                    },
                    Fields = new List<string>()
                    {
                        "Division of Process & Environmental Engineering",
                        "Division of Chemical Technology and Applied Physical Chemistry",
                        "Division of Materials Science and Technology"
                    }
                }
            };

            foreach (var department in papaDepartmentRequests)
            {
                var result = await Controller.AddDepartmentAsync(department);

                if (result.Value is not null)
                    PaPaDepartments.Add(result.Value);
            } 

            #endregion
        }

        /// <summary>
        /// Adds secretaries to the DB
        /// </summary>
        /// <returns></returns>
        public async Task AddSecretariesAsync()
        {
            #region CEID

            var ceidSecretaryUserRequests = new List<UserRequestModel>()
            {
                new()
                {
                    Username = "joanYian",
                    PasswordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                    FirstName = "Iωάννα",
                    LastName = "Γιαννακοπούλου",
                    Email = "ioanna@ceid.upatras.gr",
                    Color = "4056F4",
                    DateOfBirth = new DateOnly(24, 8, 2000),
                    PhoneNumber = new(30, "2610996941"),
                    Location = new() { Latitude = 38.29026909897528, Longitude = 21.795208116836363 }
                },
                new()
                {
                    Username = "vreAngel",
                    PasswordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                    FirstName = "Αγγελική",
                    LastName = "Βρη",
                    Email = "aggeliki@ceid.upatras.gr",
                    Color = "470FF4",
                    DateOfBirth = new DateOnly(24, 8, 2000),
                    PhoneNumber = new(30, "2610996940"),
                    Location = new() { Latitude = 38.29026909897528, Longitude = 21.795208116836363 }
                },
                new()
                {
                    Username = "maryJim",
                    PasswordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                    FirstName = "Μαρία",
                    LastName = "Δημητροπούλου",
                    Email = "dimimar@ceid.upatras.gr",
                    Color = "CEBBC9",
                    DateOfBirth = new DateOnly(24, 8, 2000),
                    PhoneNumber = new(30, "2610996939"),
                    Location = new() { Latitude = 38.29026909897528, Longitude = 21.795208116836363 }
                },
                new()
                {
                    Username = "conanBarbarian",
                    PasswordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                    FirstName = "Μαριετίνα",
                    LastName = "Βαρβαρίγου",
                    Email = "varvarigou@ceid.upatras.gr",
                    Color = "CE2D4F",
                    DateOfBirth = new DateOnly(24, 8, 2000),
                    PhoneNumber = new(30, "2610996945"),
                    Location = new() { Latitude = 38.29026909897528, Longitude = 21.795208116836363 }
                },
                new()
                {
                    Username = "anastasiaReading",
                    PasswordHash = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                    FirstName = "Αναστασία",
                    LastName = "Αναγνωστοπούλου",
                    Email = "secretary.undergraduate@ceid.upatras.gr",
                    Color = "BF1363",
                    DateOfBirth = new DateOnly(24, 8, 2000),
                    PhoneNumber = new(30, "2610996955"),
                    Location = new() { Latitude = 38.29026909897528, Longitude = 21.795208116836363 }
                }
            };

            var ceidSecretaryUsers = new List<UserResponseModel>();

            foreach (var user in ceidSecretaryUserRequests)
            {
                var result = await Controller.AddUserAsync(user);
                if (result.Value is not null)
                    ceidSecretaryUsers.Add(result.Value);
            }

            var ceidSecretaryRequests = new List<SecretaryRequestModel>()
            {
                new()
                {
                    UserId = ceidSecretaryUsers[0].Id,
                    DepartmentId = PaPaDepartments[0].Id,
                    Role = SecretaryRole.Secretary,
                },
                new()
                {
                    UserId = ceidSecretaryUsers[1].Id,
                    DepartmentId = PaPaDepartments[0].Id,
                    Role = SecretaryRole.PostGraduateSecretary,
                },
                new()
                {
                    UserId = ceidSecretaryUsers[2].Id,
                    DepartmentId = PaPaDepartments[0].Id,
                    Role = SecretaryRole.UnderGraduateSecretary,
                },
                new()
                {
                    UserId = ceidSecretaryUsers[3].Id,
                    DepartmentId = PaPaDepartments[0].Id,
                    Role = SecretaryRole.PostGraduateSecretary,
                },
                new()
                {
                    UserId = ceidSecretaryUsers[4].Id,
                    DepartmentId = PaPaDepartments[0].Id,
                    Role = SecretaryRole.UnderGraduateSecretary,
                }
            };

            var ceidSecretaries = new List<SecretaryResponseModel>();

            foreach (var secretary in ceidSecretaryRequests)
            {
                var result = await Controller.AddSecretaryAsync(secretary);
                if (result.Value is not null)
                    ceidSecretaries.Add(result.Value);
            } 

            #endregion
        }

        #endregion
    }
}
