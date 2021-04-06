The .exe this compiles to can email you when a CVS vaccine becomes available in MN based on settings defined in your apsettings.json.

###Setup 

1. Enable you gmail account to send emails from a console app. https://support.google.com/accounts/answer/6010255?hl=en-GB
2. Copy the appsettings.template.json to an apsettings.json and fill out the relevant values.
4. Build the application via `dotnet publish` (you need the runtime, take not of the output exe path).
3. Schedule the application executable. Windows: https://www.windowscentral.com/how-create-automated-task-using-task-scheduler-windows-10