#load "Crawler.fs"
open Crawler;
open System.IO;
open System.Net.Mail

let args2 = "http://www.savvyrow.co.uk/collections/latest"

let news = Crawler.diff @"c:\workspace\SavvyCrawler\latest.txt" args2

if not (Seq.isEmpty news) then
    let smtp = new SmtpClient("smtp.***.dk");
    smtp.Send(new MailMessage("ahl@***.dk", "ahl@***.dk", "SavvyRow", news |> Seq.fold (fun s x -> s + "\n\nhttp://www.savvyrow.co.uk" + x) args2 ))