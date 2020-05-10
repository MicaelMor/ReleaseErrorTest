// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.
namespace ReleaseErrorTest

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App = 
    type Model = 
      { ErrorMessage: string }

    type Msg = 
        | GetContext 

    let initModel = { ErrorMessage = "Press Button" }

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
        | GetContext ->
            try
                let ctx = DbConnection.getDbContext()
                {model with ErrorMessage = "No error, it worked"} , Cmd.none
            with
            | e -> {model with ErrorMessage = e.Message}, Cmd.none

    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(padding = Thickness 20.0, verticalOptions = LayoutOptions.Center,
            children = [ 
                View.Label(text = sprintf "%s" model.ErrorMessage, horizontalOptions = LayoutOptions.Center, width=300.0, horizontalTextAlignment=TextAlignment.Center)
                View.Button(text = "GetContext", command = (fun () -> dispatch GetContext), horizontalOptions = LayoutOptions.Center)
            ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
        |> Program.withConsoleTrace
        |> XamarinFormsProgram.run app