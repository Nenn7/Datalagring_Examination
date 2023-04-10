using Datalagring_examination.Models;
using Datalagring_examination.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace Datalagring_examination.Services;

internal class ProgramServices
{
	private readonly CustomerService _customerService = new();
	private readonly CaseService _caseService = new();
	private readonly CommentService _commentService = new();

	public async Task Main()
	{
		//Main menu funcitonality

		Console.WriteLine("---------------Huvudmeny---------------");
		Console.WriteLine("Välj ett av 5 alternativ: \n 1: Skapa nytt ärende \n 2: Skapa kommentar till ärende \n 3: Se alla ärenden \n 4: Hämta ärende med ID \n 5: Ändra status på ärende");

		var input = Console.ReadLine();

		switch (input)
		{
			case "1":
				await CreateCase();
				break;

			case "2":
				await CommentCase();
				break;

			case "3":
				await GetCases();
				break;

			case "4":
				await GetSingularCase();
				break;

			case "5":
				await ChangeStatus();
				break;

			default:
				Console.WriteLine("Vänligen ange ett nummer mellan 1-5");
				break;
		}
	}

	public async Task CreateCase()
	{
		//Creates a new customer and case

		Console.Clear();

		CaseModel newCase = new();
		CustomerModel newCustomer = new();

		Console.WriteLine("Ange ditt förnamn: ");
		newCustomer.FirstName = Console.ReadLine() ?? "";

		Console.WriteLine("Ange efternamn: ");
		newCustomer.LastName = Console.ReadLine() ?? "";

		Console.WriteLine("Ange mejladress: ");
		newCustomer.Email = Console.ReadLine() ?? "";

		Console.WriteLine("Ange mobilnummer: ");
		newCustomer.PhoneNumber = Console.ReadLine() ?? "";

		Console.WriteLine("Beskriv ditt ärende: ");
		newCase.CaseDescription = Console.ReadLine() ?? "";

		Console.WriteLine("Sparar ärende...");
		var result = await _customerService.CreateAsync(newCustomer);

		newCase.CustomerId = result.Id;

		CaseEntity caseResult = await _caseService.CreateAsync(newCase);

		Console.Clear();

		Console.WriteLine($"Ärende med ID: {caseResult.Id}, lades till för kund: {result.FirstName} {result.LastName}");
		Console.ReadKey();

	}

	public async Task CommentCase()
	{

		//Gets case by id and adds a new comment onto it based on input

		Console.Clear();

		Console.WriteLine("Vänligen ange ID-nummer för ärendet kommentar önskas läggas till på: ");
		int caseId = Int32.Parse(Console.ReadLine()	?? "");

		Console.WriteLine("Söker ärende...");

		CaseEntity searchedCase = await _caseService.GetAsync(x => x.Id == caseId);

		if (searchedCase != null)
		{
			Console.Clear();

			Console.WriteLine($"Hittat ärende med ID: {caseId}");
			Console.WriteLine($"Tillhörande kund: {searchedCase.Customer.FirstName} {searchedCase.Customer.LastName} \nÄrendebeskrivning: {searchedCase.CaseDescription} ");

			Console.WriteLine("Vänligen ange kommentar till ärendet:");

			CommentEntity comment = new();

			comment.Comment = Console.ReadLine() ?? "";

			comment.CaseId = searchedCase.Id;

			await _commentService.CreateAsync(comment);
		}
		else 
			Console.WriteLine($"Inget ärende med ID {caseId} hittades.");

	}

	public async Task GetCases()
	{
		//Outputs all existing cases and some basic information about them (including comments)

		Console.Clear();

		Console.WriteLine("Hämtar ärenden...");

		IEnumerable<CaseEntity> caseList = await _caseService.GetAllAsync();

		foreach(CaseEntity caseEntity in caseList) {
			Console.WriteLine($"ÄrendeID: {caseEntity.Id}");
			Console.WriteLine($"Registrerat namn: {caseEntity.Customer.FirstName} {caseEntity.Customer.LastName} \nBeskrivning av ärende: {caseEntity.CaseDescription} \n");

			foreach(CommentEntity comment in caseEntity.Comments)
			{
				Console.WriteLine($"Kommentar: {comment.Comment} ({comment.Created})\n");
			}
		}

	}

	public async Task GetSingularCase()
	{
		//Searches case by id and displays all of its information

		Console.Clear();

		Console.WriteLine("Ange ID för ärendet du önskar söka efter: ");

		int caseId = Int32.Parse(Console.ReadLine());

		Console.WriteLine("Söker ärende...");

		CaseEntity searchedCase = await _caseService.GetAsync(x => x.Id == caseId);

		if (searchedCase != null)
		{
			Console.Clear();

			Console.WriteLine("Hittat ärende: ");

			Console.WriteLine($"KundID: {searchedCase.Customer.Id} \nKund: {searchedCase.Customer.FirstName} {searchedCase.Customer.LastName} \nMejladress: {searchedCase.Customer.Email} \nTelefonnummer: {searchedCase.Customer.PhoneNumber}");

			Console.WriteLine($"\nÄrendeID: {searchedCase.Id} \nSkapat: {searchedCase.Created} \nSenast ändrat: {searchedCase.Modified} \nÄrendebeskrivning: {searchedCase.CaseDescription} \nStatus: {searchedCase.Status.Status}");
			foreach (CommentEntity comment in searchedCase.Comments)
			{
				Console.WriteLine($"\nKommentar: {comment.Comment} ({comment.Created})");
			}
		}
		else
			Console.WriteLine($"Inget ärende med iD {caseId} hittades");
	}

	public async Task ChangeStatus()
	{
		//Searches case by id and enables changing its status

		Console.Clear();

		Console.WriteLine("Vänligen ange ID för ärendet du vill ändra status för: ");

		int caseId = Int32.Parse(Console.ReadLine() ?? "0");

		CaseEntity searchedCase = await _caseService.GetAsync(x => x.Id == caseId);

		if (searchedCase != null)
		{
			Console.WriteLine("Hittat ärende: ");

			Console.WriteLine($"KundID: {searchedCase.Customer.Id} \nKund: {searchedCase.Customer.FirstName} {searchedCase.Customer.LastName}");

			Console.WriteLine($"\nÄrendeID: {searchedCase.Id} \nSkapat: {searchedCase.Created} \nSenast ändrat: {searchedCase.Modified} \nÄrendebeskrivning: {searchedCase.CaseDescription} \nStatus: {searchedCase.Status.Status}");

			Console.WriteLine("\nAnge vilken status ärendet ska ändras till: \n 1: 'Ej påbörjad' \n 2: 'Pågående' \n 3: 'Avslutad'");

			var input = Int32.Parse(Console.ReadLine() ?? "0");

			CaseEntity updatedCase = await _caseService.UpdateCaseStatusAsync(x => x.Id == caseId, input);

			Console.WriteLine($"Ärendets statusID har ändrats till: {updatedCase.StatusId}");
		}

		else
			Console.WriteLine($"Inget ärende med ID {caseId} hittades.");
		
	}
}
