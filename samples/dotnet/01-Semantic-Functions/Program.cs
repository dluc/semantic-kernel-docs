﻿using Microsoft.SemanticKernel;

var kernelSettings = KernelSettings.LoadSettings();
IKernel kernel = new KernelBuilder()
    .WithCompletionService(kernelSettings)
    .Build();

var pluginsDirectory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "plugins");

// Import the OrchestratorPlugin and SummarizeSkill from the plugins directory.
var orchestrationPlugin = kernel.ImportSemanticSkillFromDirectory(pluginsDirectory, "OrchestratorPlugin");
var summarizationPlugin = kernel.ImportSemanticSkillFromDirectory(pluginsDirectory, "SummarizeSkill");

// Create a new context and set the input, history, and options variables.
var context = kernel.CreateNewContext();
context["input"] = "Yes";
context["history"] = @"Bot: How can I help you?
User: My team just hit a major milestone and I would like to send them a message to congratulate them.
Bot:Would you like to send an email?";
context["options"] = "SendEmail, ReadEmail, SendMeeting, RsvpToMeeting, SendChat";

// Run the Summarize function with the context.
var result = await orchestrationPlugin["GetIntent"].InvokeAsync(context);

Console.WriteLine(result);