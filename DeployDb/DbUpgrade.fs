module DbUpgrade
open System.Reflection
open DbUp

module FarmerHelpers =
  let sqlServerPasswordParameter serverName adminPassword = (sprintf "password-for-%s" serverName),adminPassword

let tryExecute =
  Result.bind (fun (outputs:Map<string,string>) ->
    try
      let connectionString = outputs.["connection-string"]
      let result =
        DeployChanges
          .To
          .SqlDatabase(connectionString)
          .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
          .LogToConsole()
          .Build()
          .PerformUpgrade()
      match result.Successful with
      | true -> Ok outputs
      | false -> Error (sprintf "%s: %s" (result.Error.GetType().Name.ToUpper()) result.Error.Message)
    with _ -> Error "Unexpected error occurred upgrading database"
  )

