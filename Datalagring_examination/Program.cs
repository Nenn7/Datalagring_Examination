using Datalagring_examination.Services;

StatusService statusService = new();

await statusService.InitializeAsync();


//Keeps service running in case user wants to continue after completing a task

ProgramServices programServices = new();

bool serviceRunning = true;

while (serviceRunning == true) { 

	await programServices.Main();

	Console.WriteLine("\nÖnskar du avsluta servicen? \n 'J' för ja  / 'N' för nej");
	var input = Console.ReadLine().ToUpper();

	switch(input)
	{
		case "J":
			serviceRunning = false;
			break;

		case "N":
			serviceRunning = true;
			break;

		default:
			Console.WriteLine("Ogiltigt svar");
			Console.ReadLine();
			break;
	}

	Console.Clear();
}