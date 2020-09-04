module Helpers

let createSqlServerPasswordParameter serverName adminPassword = (sprintf "password-for-%s" serverName),adminPassword

let asGitHubAction = function | Ok _ -> 0 | Error e -> (printf "%s" e) ; 1
