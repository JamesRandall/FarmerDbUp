open System
open Farmer
open Farmer.Builders
open Sql
open Constants
open Helpers

[<EntryPoint>]
let main _ =
  let adminPasswordParameter =
    Environment.GetEnvironmentVariable("ADMIN_PASSWORD") |> createSqlServerPasswordParameter serverName

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
    output "connection-string" (demoDatabase.ConnectionString databaseName)
  }

  let deploymentPipeline =
    Deploy.tryExecute "demoResourceGroup" [ adminPasswordParameter ]
    >> DbUpgrade.tryExecute 
  
  template |> deploymentPipeline |> asGitHubAction
