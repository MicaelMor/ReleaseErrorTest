namespace ReleaseErrorTest

module DbConnection =

    open System.Data
    open FSharp.Data.Sql
    open FSharp.Core


    let [<Literal>] dbVendor = Common.DatabaseProviderTypes.POSTGRESQL

    let [<Literal>] connString = "Host=34.105.253.30;Port=5432;Database=postgres;Username=postgres;Password=vrt5LppzCi4dDMKB"
    
    let [<Literal>] resPath = __SOURCE_DIRECTORY__  + @"/../npgsql/"

    let [<Literal>] indivAmount = 1000

    let [<Literal>] useOptTypes  = true

    let [<Literal>] owner = "public"

    type DbMdb =
        SqlDataProvider<dbVendor, connString,"",resPath,indivAmount,useOptTypes,owner>

    let getDbContext() = DbMdb.GetDataContext()