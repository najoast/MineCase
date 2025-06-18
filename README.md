MineCase 
======================================
#### [![.NET Build Status](https://github.com/najoast/MineCase/actions/workflows/dotnet.yml/badge.svg)](https://github.com/najoast/MineCase/actions/workflows/dotnet.yml)

![Logo](doc/logo/MineCaseLogo.png)

`MineCase` is a `Minecraft` server implement in dotnet core. 

This project is designed to create a high-performance, distributed `Minecraft` server with virtual actor provided by Orleans distributed framework. 

Different chunks are managed on different servers so that more players can join in and play in the same world. This makes minecraft servers more scalable.

Servers like Anarchy servers can allow more players to join in without waiting in queue by using distributed server.

It written in `C#` with `.NET 9.0` env and based on `Orleans 9.x` framework to work with released [1.15.2 protocol](https://www.minecraft.net/en-us/article/minecraft-java-edition-1-15-2). The [website](https://wiki.vg/) describes the Minecraft protocol clearly.

**MineCase is under refactoring, so branch refactor may not work.**

**MineCase is not stable and lack of many features now. Please don't use MineCase in production unless you know what you're doing.**

![Screenshots](screenshots/1.jpg)

## Run Requirements
* [.Net 9.0](https://www.microsoft.com/net/download)
* [MongoDB](https://www.mongodb.com/download-center/community)

## Install (Build From Source)
1. Download and install a `.NET 9.0` from this [page](https://www.microsoft.com/net/download).
2. Download and install a `MongoDB` from this [page](https://www.mongodb.com/download-center?jmp=nav#community).
3. Download a `MineCase` archive from the Release Page (or **clone:**)
	```bash
	git clone https://github.com/najoast/MineCase.git
	cd MineCase
	```
4. Build and run the `build_and_run`
    * **OSX** : Run the `build_and_run.sh`.
    * **Linux** : Run the `build_and_run.sh`.
    * **Win** : Double-click `build_and_run.bat`.

## How it works
None

## Contributors
[![sunnycase](https://i.loli.net/2020/02/19/QWGu4759qeUam8c.png)](https://github.com/sunnycase)[![jstzwj](https://i.loli.net/2020/02/19/kSqmT7cFfp5Qi4L.png)](https://github.com/jstzwj)[![akemimadoka](https://i.loli.net/2020/02/19/s2GmUF7SwqzC9ER.png)](https://github.com/akemimadoka)[![Alinshans](https://i.loli.net/2020/02/19/yt9DE4LT1RkweQb.png)](https://github.com/Alinshans)[![ray-cast](https://i.loli.net/2020/02/19/r42VmKzjlpaQPCc.png)](https://github.com/ray-cast)[![Melonpi](https://i.loli.net/2020/02/19/KcW4pes71AR5bqH.png)](https://github.com/Melonpi)[![zaoqi](https://i.loli.net/2020/02/19/15ByH8UoICESudh.png)](https://github.com/zaoqi)

## Get Involved

We need help to make MineCase better. You can help us by fixing bugs, developing new features, improving documents.  
Some new contributors wonder what to work. The project began with the love for Minecraft, so our answer is always "do what you love". 

## Contact
This project is still under development. 

If you have any questions we can discuss together in the [Issues](https://github.com/najoast/MineCase/issues). Also any questions you may have while using this server, or any good suggestions, can be addressed to us in Issues.

We welcome and appreciate your contributions to this project.

* Discord : [MineCase](https://discord.gg/8Z5RSRn)

## Contributors

### Code Contributors

This project exists thanks to all the people who contribute. [[Contribute](CONTRIBUTING.md)].
<a href="https://github.com/dotnetGame/MineCase/graphs/contributors"><img src="https://opencollective.com/MineCase/contributors.svg?width=890&button=false" /></a>

### Financial Contributors

Become a financial contributor and help us sustain our community. [[Contribute](https://opencollective.com/MineCase/contribute)]

#### Individuals

<a href="https://opencollective.com/MineCase"><img src="https://opencollective.com/MineCase/individuals.svg?width=890"></a>

#### Organizations

Support this project with your organization. Your logo will show up here with a link to your website. [[Contribute](https://opencollective.com/MineCase/contribute)]

<a href="https://opencollective.com/MineCase/organization/0/website"><img src="https://opencollective.com/MineCase/organization/0/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/1/website"><img src="https://opencollective.com/MineCase/organization/1/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/2/website"><img src="https://opencollective.com/MineCase/organization/2/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/3/website"><img src="https://opencollective.com/MineCase/organization/3/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/4/website"><img src="https://opencollective.com/MineCase/organization/4/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/5/website"><img src="https://opencollective.com/MineCase/organization/5/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/6/website"><img src="https://opencollective.com/MineCase/organization/6/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/7/website"><img src="https://opencollective.com/MineCase/organization/7/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/8/website"><img src="https://opencollective.com/MineCase/organization/8/avatar.svg"></a>
<a href="https://opencollective.com/MineCase/organization/9/website"><img src="https://opencollective.com/MineCase/organization/9/avatar.svg"></a>
