open System
open Farmer
open Farmer.Builders
open Sql
open Constants

let asGitHubAction = function | Ok _ -> 0 | Error e -> (printf "%s" e) ; 1

[<EntryPoint>]
let main _ =
  let sqlServerPasswordParameter =
    Environment.GetEnvironmentVariable("ADMIN_PASSWORD") |> DbUpgrade.FarmerHelpers.sqlServerPasswordParameter serverName

  let demoDatabase = sqlServer {
    name serverName
    admin_username "demoAdmin"
    enable_azure_firewall
      
    add_databases [
      sqlDb { name databaseName ; sku DbSku.Basic }
    ]
  }

  let template = arm {
    location Location.UKWest
    add_resource demoDatabase
    output "connection-string" (demoDatabase.ConnectionString "demoDb")
  }

  let deploymentPipeline =
    Deploy.tryExecute "demoResourceGroup" [ sqlServerPasswordParameter ]
    >> DbUpgrade.tryExecute 
  
  template |> deploymentPipeline |> asGitHubAction
