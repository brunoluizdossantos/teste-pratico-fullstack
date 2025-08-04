namespace Domain.Utils;

public static class Constants
{
	public const string ProjectTitle = "Teste Prático";
	public const string ProjectDescription = "Projeto desenvolvido com ASP.Net 8, C# e Angular 19";
	public const string ProjectContactName = "Bruno Luiz";
	public const string ProjectContactEmail = "brunoluizdossantos@gmail.com";
	public const string ProjectLicenseName = "MIT License";
	public const string ProjectLicenseUrl = "https://opensource.org/licenses/MIT";
	public const string ProjectServiceName = "teste-pratico-api";

	public const string IdProperty = "Id";
	public const string IdRequiredError = $"{IdProperty} inválido. {IdProperty} é obrigatório";

	public const string UserIdProperty = "Id do usuário";
	public const string UserIdRequiredError = $"{UserIdProperty} inválido. {UserIdProperty} é obrigatório";

	public const string NameProperty = "Nome";
	public const int NameMinLength = 3;
	public const int NameMaxLength = 100;
	public const string NameRequiredError = $"{NameProperty} inválido. {NameProperty} é obrigatório";
	public const string NameMinLengthError = $"{NameProperty} inválido, muito curto, mínimo de 3 caracteres";
	public const string NameMaxLengthError = $"{NameProperty} inválido, muito longo, máximo de 100 caracteres";

	public const string EmailProperty = "E-mail";
	public const int EmailMinLength = 3;
	public const int EmailMaxLength = 100;
	public const string EmailRequiredError = $"{EmailProperty} inválido. {EmailProperty} é obrigatório";
	public const string EmailMinLengthError = $"{EmailProperty} inválido, muito curto, mínimo de 3 caracteres";
	public const string EmailMaxLengthError = $"{EmailProperty} inválido, muito longo, máximo de 100 caracteres";

	public const string HashProperty = "Hash";
	public const int HashMinLength = 3;
	public const int HashMaxLength = 100;
	public const string HashRequiredError = $"{HashProperty} inválido. {HashProperty} é obrigatório";
	public const string HashMinLengthError = $"{HashProperty} inválido, muito curto, mínimo de 3 caracteres";
	public const string HashMaxLengthError = $"{HashProperty} inválido, muito longo, máximo de 100 caracteres";

	public const string SaltProperty = "Valor randômico";
	public const int SaltMinLength = 3;
	public const int SaltMaxLength = 20;
	public const string SaltRequiredError = $"{SaltProperty} inválido. {SaltProperty} é obrigatório";
	public const string SaltMinLengthError = $"{SaltProperty} inválido, muito curto, mínimo de 3 caracteres";
	public const string SaltMaxLengthError = $"{SaltProperty} inválido, muito longo, máximo de 20 caracteres";

	public const string PasswordProperty = "Senha";
	public const int PasswordMinLength = 3;
	public const int PasswordMaxLength = 100;
	public const string PasswordRequiredError = $"{PasswordProperty} inválida. {PasswordProperty} é obrigatório";
	public const string PasswordMinLengthError = $"{PasswordProperty} inválida, muito curto, mínimo de 3 caracteres";
	public const string PasswordMaxLengthError = $"{PasswordProperty} inválida, muito longo, máximo de 100 caracteres";

	public const string UserNotFound = "Usuário não encontrado";
	public const string UserEmailExists = "Já existe um usuário cadastrado com este e-mail";
	public const string UserInvalidPassword = "Senha inválida para o usuário informado";

	public const string UserListSummary = "Retorna todos os usuários";
	public const string UserListDescription = "Retorna todas os usuários do sistema";
	public const string UserListError = "Erro ao tentar obter os usuários";

	public const string UserCreateSummary = "Cadastra novo usuário";
	public const string UserCreateDescription = "Cadastra novo usuário do sistema";
	public const string UserCreateError = "Erro ao tentar cadastrar usuário";

	public const string UserItemSummary = "Retorna um usuário";
	public const string UserItemDescription = "Retorna um usuário do sistema pelo ID";
	public const string UserItemError = "Erro ao tentar obter o usuário";

	public const string UserUpdateSummary = "Atualiza um usuário";
	public const string UserUpdateDescription = "Atualiza um usuário do sistema pelo ID";
	public const string UserUpdateError = "Erro ao tentar atualizar usuário";

	public const string UserSchemaName = "Nome do usuário";
	public const string UserSchemaNameValue = "João";
	public const string UserSchemaEmail = "E-mail do usuário";
	public const string UserSchemaEmailValue = "joao@teste.com";
	public const string UserSchemaHashedPassword = "$2a$11$3umVJKFsM8y8/u1Jy46vlehKTmluTpNJuiMbif5FvsA4OhO5kaoJS";
	public const string UserSchemaSalt = "11233885";

	public const string UserRepositoryTestGetAllItems = "Valida a obtenção de uma lista de usuários";
	public const string UserRepositoryTestGetById = "Valida a obtenção de um usuário pelo ID";
	public const string UserRepositoryTestCreate = "Valida o cadastro de um usuário";

	public const string UserServiceTestGetAllItems = "Retorna uma lista de usuários";
	public const string UserServiceTestGetById = "Retorna um usuário pelo ID";
	public const string UserServiceTestCreate = "Cadastra e retorna um usuário";
	public const string UserServiceTestUpdate = "Atualiza e retorna um usuário";

	public const string UserUnitTestCreateWithValidParameters = "Cadastra usuário válido com parâmetros válidos";
	public const string UserUnitTestValidShortNameValue = "Valida usuário com nome muito curto";
	public const string UserUnitTestValidLongNameValue = "Valida usuário com nome muito longo";
	public const string UserUnitTestValidMissingNameValue = "Valida usuário com nome não preenchido";
	public const string UserUnitTestValidNullNameValue = "Valida usuário com nome nulo";
	public const string UserUnitTestValidShortEmailValue = "Valida usuário com e-mail muito curto";
	public const string UserUnitTestValidLongEmailValue = "Valida usuário com e-mail muito longo";
	public const string UserUnitTestValidMissingEmailValue = "Valida usuário com e-mail não preenchido";
	public const string UserUnitTestValidNullEmailValue = "Valida usuário com e-mail nulo";

	public const string AuthTokenSchemaSummary = "Retorna o token de acesso";
	public const string AuthTokenSchemaDescription = "Retorna o token de acesso do sistema";
	public const string AuthTokenSchemaError = "Erro ao tentar obter o token de acesso";
	public const string AuthTokenSchemaNotFound = "Token de acesso não encontrado na resposta";
	public const string AuthTokenSchemaInvalid = "Resposta inválida do servidor de autenticação";

	public const string LoginSchemaEmailValue = "usuario@teste.com";
	public const string LoginSchemaPasswordValue = "senha1";

	public const string RequestSchemaPageNumberValue = "1";
	public const string RequestSchemaPageSizeValue = "8";
	public const string RequestSchemaTotalPagesValue = "0";
	public const string RequestSchemaTotalRecordsValue = "0";

	public const string DefaultSchemaIdValue = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
	public const string DefaultSchemaBoolValue = "true";
	public const int DefaultPasswordLength = 10;

	public const string JwtAuthorizationDescription = "Cabeçalho de autorização JWT usando o esquema Bearer";

	public const string TestAllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#@$^*()";

	public const string PageNumberProperty = "Número da página";
	public const int PageNumberMinValue = 1;
	public const string PageNumberRequiredError = $"{PageNumberProperty} inválido. {PageNumberProperty} é obrigatório";
	public const string PageNumberMinValueError = $"{PageNumberProperty} inválido. {PageNumberProperty} deve ser maior ou igual a 1";

	public const string PageSizeProperty = "Tamanho da página";
	public const int PageSizeMinValue = 1;
	public const string PageSizeRequiredError = $"{PageSizeProperty} inválido. {PageSizeProperty} é obrigatório";
	public const string PageSizeMinValueError = $"{PageSizeProperty} inválido. {PageSizeProperty} deve ser maior ou igual a 1";

	public const string TotalPagesProperty = "Total de páginas";

	public const string TotalRecordsProperty = "Total de registros";

	public const string EntityErrorNotCreated = "Erro ao criar entidade";
	public const string EntityErrorNotLoaded = "Erro ao carregar a entidade";
	public const string EntityErrorNotFound = "Entidade não encontrada";
}
