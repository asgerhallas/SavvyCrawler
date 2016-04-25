module Crawler

    open System;
    open System.IO;
    open FSharp.Data;

    let crawl (url:string) = 
        let html = HtmlDocument.Load(url);
        let articles = html.Descendants ["article"]
        let links = 
            articles
            |> Seq.collect (fun x -> x.Descendants "figure")
            |> Seq.map (fun x -> x.Elements "a")
            |> Seq.concat
            |> Seq.map (fun (y:HtmlNode) -> y.TryGetAttribute "href")
            |> Seq.choose (Option.map (fun (x:HtmlAttribute) -> x.Value()))
        links

    let diff path url =
        let previous = if File.Exists path then File.ReadAllLines path else Array.empty
        let current = crawl url
        let news = Seq.except previous current 
        File.WriteAllLines(path, current)
        news
    