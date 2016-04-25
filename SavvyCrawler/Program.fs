open Crawler;
open System;
open System.Net.Mail

[<EntryPoint>]
let main argv = 
    let news = Crawler.diff argv.[0] argv.[1]
    if not (Seq.isEmpty news) then
        let smtp = new SmtpClient("smtp.***.dk");
        smtp.Send(new MailMessage("ahl@***.dk", "ahl@***.dk", "SavvyRow", news |> Seq.fold (fun s x -> s + "\n\nhttp://www.savvyrow.co.uk" + x) argv.[1] ))
    0