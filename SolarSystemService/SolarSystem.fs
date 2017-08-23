module SolarSystem

open FSharp.Data

type SolarSystemItem = 
    {
        Name : string
        Image : string
        Size : decimal
        Facts : string
    }

type SolarSystem = JsonProvider<"https://solarsystem.nasa.gov/json/page-json.cfm?URLPath=planets/neptune/facts">

let getFacts url =
    SolarSystem.Load("https://solarsystem.nasa.gov/json/page-json.cfm?URLPath=" + url + "/facts").Main.Content

type PlanetsGal = JsonProvider<"https://solarsystem.nasa.gov/data/planetsGal.json">

let sample = PlanetsGal.GetSample();

let Planets = 
    sample.GalPlanets
    |> Array.map (fun planet -> 
        { 
            Name = planet.Name
            Image = "https://solarsystem.nasa.gov/" + planet.Img
            Size = decimal planet.Size
            Facts = planet.Url |> getFacts 
        })

let Moons = 
    sample.GalMoons
    |> Array.map (fun moon -> 
        { 
            Name = moon.Name
            Image = "https://solarsystem.nasa.gov/" + moon.Img
            Size = moon.Size
            Facts = ""
        })

let Regions = 
    sample.GalRegions
    |> Array.map (fun region -> 
        { 
            Name = region.Name
            Image = "https://solarsystem.nasa.gov/" + region.Img
            Size = region.Size |> Option.defaultValue 0.0m
            Facts = region.Url |> Option.map getFacts |>  Option.defaultValue ""
        })

let SmallBodies = 
    sample.GalSmallBodies
    |> Array.map (fun smallBody -> 
        { 
            Name = smallBody.Name
            Image = "https://solarsystem.nasa.gov/" + smallBody.Img
            Size = decimal smallBody.Size
            Facts = smallBody.Url |> getFacts
        })

let Stars = 
    sample.GalStars
    |> Array.map (fun star -> 
        { 
            Name = star.Name
            Image = "https://solarsystem.nasa.gov/" + star.Img
            Size = star.Size
            Facts = star.Url |> getFacts
        })

type AstroPhys = JsonProvider<"http://www.astro-phys.com/api/de406/states?date=2017-08-22&bodies=sun,mercury,venus,earth,moon,earthmoon,geomoon,mars,jupiter,uranus,neptune,pluto">
let ss = AstroPhys.GetSample().Results.Earth

type Wikipedia = HtmlProvider<"https://en.wikipedia.org/wiki/Earth">
let earth = Wikipedia.GetSample().Tables.``Earth ``

type GalleryItem =
    {
        Name : string
        Url : string
        Image : string
    }

type Gallery = JsonProvider<"https://solarsystem.nasa.gov/json/page-json.cfm?URLPath=galleries/">
let Galleries = 
    Gallery.GetSample().GalleriesList
    |> Array.collect (fun gallery ->
        gallery.GalleryLinks 
        |> Array.map (fun link -> 
            {
                Name = link.Title
                Url = link.Url
                Image = "https://solarsystem.nasa.gov/" + link.Image
            }))

type GalleryImages = JsonProvider<"https://solarsystem.nasa.gov/json/page-json.cfm?URLPath=galleries/search/&category=planets">
let getImages url =
    GalleryImages.Load("https://solarsystem.nasa.gov/json/page-json.cfm?URLPath=" + url).Gallery
    |> Array.map (fun image ->
        {
            Name = image.Title
            Url = null
            Image = "https://solarsystem.nasa.gov/" + image.Image
        })