﻿module CustomHostedServices

open System
open System.Threading.Tasks
open System.Timers
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

[<AbstractClass>]
type TimedHostedService(timer : Timer, logger : ILogger) =
    abstract member doWork : obj -> unit
    
    interface IHostedService with
        member this.StartAsync _ =
            logger.LogInformation "Timed Hosted Service running."
            timer.Elapsed.Add this.doWork
            // do work is not started immediately after call. it'll wait until first tick
            this.doWork ()
            // so we force start
            timer.Start ()
            Task.CompletedTask
        
        member this.StopAsync _ =
            logger.LogInformation "Timed Hosted Service is stopping."
            timer.Stop ()
            Task.CompletedTask 
            
    interface IDisposable with
        member this.Dispose() =
            timer.Dispose()
            