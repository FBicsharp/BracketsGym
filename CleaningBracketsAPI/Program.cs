using CleaningBracketsAPI.Logic;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
#endif
var app = builder.Build();

#if DEBUG
	app.UseSwagger();
	app.UseSwaggerUI();
#endif

app.UseHttpsRedirection();

app.MapPost("/cleanbrackets", async (List<string> inputString) =>
{	
	await Console.Out.WriteLineAsync(string.Join("\n", inputString));
	var bracketsCleaner = new BracketsCleaner();
	return await bracketsCleaner.ProcessStringAsync(inputString);	
});
app.Run();
