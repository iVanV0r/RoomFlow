// Tests/LoginPageTests.cs
using Xunit;
using FluentAssertions;
using AngleSharp;
using AngleSharp.Html.Parser;
namespace RoomFlow.Tests
{
	public class LoginPageTests
	{

		private readonly string _loginPagePath;
		private readonly string _htmlContent;
		public LoginPageTests()
		{

			// Путь к файлу Login.cshtml
			_loginPagePath = Path.Combine(Directory.GetCurrentDirectory(),
				"..", "..", "..", "..", "RoomFlow", "Views", "Account", "Login.cshtml");


			// Читаем содержимое файла
			_htmlContent = File.ReadAllText(_loginPagePath);
		}

		[Fact]
		public void LoginPage_FileExists()
		{

			// Тест 1: Файл страницы логина существует
			File.Exists(_loginPagePath).Should().BeTrue(
				$"Файл {_loginPagePath} должен существовать");
		}

		[Fact]
		public void LoginPage_HasValidHtmlStructure()
		{

			// Тест 2: Базовая проверка структуры HTML
			_htmlContent.Should().ContainAny(
				new[] { "<!DOCTYPE html>", "<html" },
				because: "HTML должен иметь базовую структуру");


			_htmlContent.Should().ContainAny(
				new[] { "<head>", "<body>" },
				because: "HTML должен содержать основные секции");
		}

		[Fact]
		public async Task LoginPage_HasLoginForm()
		{

			// Тест 3: На странице есть форма логина
			var context = BrowsingContext.New(Configuration.Default);
			var parser = context.GetService<IHtmlParser>();
			var document = await parser.ParseDocumentAsync(_htmlContent);
			var forms = document.QuerySelectorAll("form");
			forms.Should().NotBeEmpty("На странице должна быть форма");
			// Проверяем атрибуты ASP.NET формы
			var hasAspAction = document.QuerySelectorAll("[asp-action]").Length > 0;
			var hasAspController = document.QuerySelectorAll("[asp-controller]").Length > 0;
			var hasMethodPost = document.QuerySelectorAll("form[method='post']").Length > 0;
			(hasAspAction || hasAspController || hasMethodPost).Should().BeTrue(
				"Форма должна иметь атрибуты ASP.NET или метод POST");
		}

		[Fact]
		public async Task LoginPage_HasEmailField()
		{

			// Тест 4: Есть поле для email/username
			var context = BrowsingContext.New(Configuration.Default);
			var parser = context.GetService<IHtmlParser>();
			var document = await parser.ParseDocumentAsync(_htmlContent);
			var emailFields = document.QuerySelectorAll("""
                input[type='email'], 
                input[name*='email'], 
                input[name*='Email'], 
                input[id*='email'], 
                input[id*='Email'],
                input[placeholder*='email'],
                input[placeholder*='Email'],
                [asp-for*='Email']
                """);

			emailFields.Should().NotBeEmpty(
				"Должно быть поле для email/username");
		}

		[Fact]
		public async Task LoginPage_HasPasswordField()
		{

			// Тест 5: Есть поле для пароля
			var context = BrowsingContext.New(Configuration.Default);
			var parser = context.GetService<IHtmlParser>();
			var document = await parser.ParseDocumentAsync(_htmlContent);
			var passwordFields = document.QuerySelectorAll("""
                input[type='password'], 
                input[name*='password'], 
                input[name*='Password'], 
                input[id*='password'], 
                input[id*='Password'],
                input[placeholder*='password'],
                input[placeholder*='Password'],
                [asp-for*='Password']
                """);
			passwordFields.Should().NotBeEmpty(
				"Должно быть поле для пароля");
		}

		[Fact]
		public async Task LoginPage_HasRememberMeCheckbox()
		{

			// Тест 6: Есть чекбокс 'Запомнить меня'
			var context = BrowsingContext.New(Configuration.Default);
			var parser = context.GetService<IHtmlParser>();
			var document = await parser.ParseDocumentAsync(_htmlContent);
			var rememberCheckboxes = document.QuerySelectorAll("""
                input[type='checkbox'][name*='Remember'], 
                input[type='checkbox'][name*='remember'],
                input[type='checkbox'][id*='Remember'],
                [asp-for*='RememberMe'],
                :contains('Запомнить'),
                :contains('Remember')
                """);

			rememberCheckboxes.Should().NotBeEmpty(
				"Должен быть чекбокс 'Запомнить меня'");
		}

		[Fact]
		public async Task LoginPage_HasSubmitButton()
		{
			// Тест 7: Есть кнопка отправки формы
			var context = BrowsingContext.New(Configuration.Default);
			var parser = context.GetService<IHtmlParser>();
			var document = await parser.ParseDocumentAsync(_htmlContent);

			var submitButtons = document
				.QuerySelectorAll("input[type='submit'], button[type='submit'], button")
				.Where(e =>
					e.TextContent.Contains("Войти", StringComparison.OrdinalIgnoreCase) ||
					e.TextContent.Contains("Log in", StringComparison.OrdinalIgnoreCase) ||
					e.TextContent.Contains("Login", StringComparison.OrdinalIgnoreCase) ||
					e.TextContent.Contains("Sign in", StringComparison.OrdinalIgnoreCase)
				);

			submitButtons.Should().NotBeEmpty(
				because: "Должна быть кнопка отправки формы");
		}

		[Fact]
		public async Task LoginPage_HasValidationElements()
		{
			// Тест 8: Есть элементы валидации
			var context = BrowsingContext.New(Configuration.Default);
			var parser = context.GetService<IHtmlParser>();
			var document = await parser.ParseDocumentAsync(_htmlContent);
			var validationElements = document.QuerySelectorAll("""
                [asp-validation-for],
                .text-danger,
                [data-val='true'],
                span.field-validation-error,
                .validation-message
                """);

			validationElements.Should().NotBeEmpty(
				"Должны быть элементы валидации");
		}

		[Fact]
		public async Task LoginPage_HasForgotPasswordLink()
		{
			// Тест 9: Есть ссылка "Забыли пароль?"
			var context = BrowsingContext.New(Configuration.Default);
			var parser = context.GetService<IHtmlParser>();
			var document = await parser.ParseDocumentAsync(_htmlContent);

			var forgotLinks = document
				.QuerySelectorAll("a")
				.Where(a =>
					a.TextContent.Contains("Забыли пароль", StringComparison.OrdinalIgnoreCase) ||
					a.TextContent.Contains("Forgot password", StringComparison.OrdinalIgnoreCase) ||
					(a.GetAttribute("href")?.Contains("ForgotPassword", StringComparison.OrdinalIgnoreCase) ?? false) ||
					(a.GetAttribute("asp-action")?.Contains("ForgotPassword", StringComparison.OrdinalIgnoreCase) ?? false)
				);

			forgotLinks.Should().NotBeEmpty(
				because: "Должна быть ссылка 'Забыли пароль?'");
		}

		[Fact]
		public async Task LoginPage_HasRegisterLink()
		{
			// Тест 10: Есть ссылка на регистрацию
			var context = BrowsingContext.New(Configuration.Default);
			var parser = context.GetService<IHtmlParser>();
			var document = await parser.ParseDocumentAsync(_htmlContent);

			var registerLinks = document
				.QuerySelectorAll("a")
				.Where(a =>
					a.TextContent.Contains("Зарегистрироваться", StringComparison.OrdinalIgnoreCase) ||
					a.TextContent.Contains("Register", StringComparison.OrdinalIgnoreCase) ||
					a.TextContent.Contains("Регистрация", StringComparison.OrdinalIgnoreCase) ||
					a.TextContent.Contains("Create account", StringComparison.OrdinalIgnoreCase) ||
					a.TextContent.Contains("Нет аккаунта", StringComparison.OrdinalIgnoreCase) ||
					(a.GetAttribute("href")?.Contains("Register", StringComparison.OrdinalIgnoreCase) ?? false) ||
					(a.GetAttribute("asp-action")?.Contains("Register", StringComparison.OrdinalIgnoreCase) ?? false)
				);

			registerLinks.Should().NotBeEmpty(
				because: "Должна быть ссылка на регистрацию");
		}
	}
}