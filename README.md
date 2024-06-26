Suika2 / Polaris Engine
=======================

## What is Suika2 / Polaris Engine?

Suika2 is the first-class development software for visual novel creation.
Polaris Engine is a code name of Suika2.

Suika2 has an open-source philosophy and is designed with a true cross-platform portablity including iOS, Android, even gaming consoles.
It is equipped with features and tools of production-grade quality.
Suika2 is perfect for both beginners and seasoned creators alike.

Effortlessness and efficiently are key concepts of Suika2.
It provides that without sacrificing features or usability to enrich both the creator and user experience.

Suika2 don't aim for no-code but it achieve low-code using multiple Domain Specific Languages).
Specifically, there are separate languages for scenarios, user interfaces, and logics.
We are in the process of adding a little graphical support for each DSL.
This is quite different from other engines and we call it "Visual Live Scripting" technology.
The author thinks as a researcher of software engineering that VLS is one shape of RAD, Rapid Application Development.

## What is the Suika2's Development Goal?

**"The New Standard for Visual Novel Creation"**, in the era of 2020s and beyond, is the current goal of the Suika2 development.

## What are the Suika2's Mission, Vision and Values (MVV)?

* Mission
  * **Simple:** Suika2 enables the creation of Visual Novels in an easy and efficient manner.
  * **Fast:** Apps are constructed using native technologies only, ensuring resource efficiency on mobile devices.
  * **Free:** We are an open source project and we make our technology public as pro-bono-publico with respects for intellectual properties of others.
* Vision
  * **Earning:** Game developers can publish their games on stores and earn income.
  * **Prosperity:** Our aim is to create a world where anyone can make a living with just a single computer including mobile devices.
  * **Talent**: Bringing people with talents but difficulties into the world (Giftedness support)
* Values
  * **Information:** We hold the belief that the creation and generation of information including story writing is humanity's true value.
  * **Market:** We shall complete the market launch of game subscriptions with world leading platform partners.
  * **Diversity:** Develop, distribute, and publish on all platforms - Suika2 seeks the true portability and we call it diversity.

## Binary Downloads

Please visit [the official Web site](https://suika2.com/en/dl/) to obtain the latest release.

Note that we have official store releases but they are very older than the Web site versions.

* For iOS:
  * Open [App Store](https://apps.apple.com/us/app/suika2-pro-mobile/id6474521680).
* For Android:
  * Open [Google Play](https://play.google.com/store/apps/details?id=jp.luxion.suikapro)

## Build

See also [Build Instructions](https://github.com/ktabata/suika2/raw/master/build/README.md) for more details.

On Ubuntu:
```
sudo apt-get install -y git build-essential libasound2-dev libx11-dev mesa-common-dev qt6-base-dev qt6-multimedia-dev libwebp-dev
git clone https://github.com/suika2engine/suika2.git
./configure
make
sudo make install
suika2
```

## Discord

We have two servers.

### User Community Server

Feel free to ask anything in this server. Any language will do.

<a href="https://discord.gg/Xh9mFwr4E8">Join our user community</a>

### Dev Server

The dev server is a cherished and invaluable space that embraces developers, creators, and end-users irrespective of their nationality, language, ethnicity, color, lineage, beliefs, sexuality, gender, education, age, religion, or identity.
That's why we'd love you to join our community! You're definitely deserved to be a part of it.
The Dev Server holds a grand vision for cultural growth and engage in profound discussions, so please try the User Community Server first.

<a href="https://discord.gg/uKr4m2k2nJ">Join our dev server</a>

## Suika2 Pro series

`Suika2 Pro Desktop` and `Suika2 Pro Mobile` are Suika2's creator tools, available for Windows PC, Mac, iPhone, iPad, Android phone/tablet, Chromebook and Linux.
They provide creators with all the functionalities they need, such as editing, debugging, and exporting games out of the box.

## License

This software is released under the MIT license.
There is no restriction on distribution and or modification of the Suika2 Source Code.

This project will never be commercialized in the future. Please use Suika2 with confidence.

## Contribution

The best way to contribute to this project is to use it and give us feedback.
We are always open to suggestions and ideas.

## Live Web Demo

[Click Here](https://suika2.com/run/sample/)

## Documentation

* [Japanese Documentation](https://suika2.com/wiki/)
* [English Documentation](https://suika2.com/en/wiki/)

## Portability

Games made with Suika2 can run on Windows PC, Mac, iPhone, iPad, Android, Web browser, Chromebook, Linux, FreeBSD, NetBSD, OpenBSD, and commercial consoles.

Suika2 consists of the platform independent core (CORE) and the hardware abstraction layer (HAL).
The CORE is written in ANSI C, the most portable programming language in the world, while HAL implementations are currently written in C, C++, Objective-C, Swift and Java.

If you would like to port Suika2 to a new target platform, you only need to write a thin HAL, this is generally possible within a week.

Suika2 does not depend on SDKs or frameworks such as Unity, Godot, Python, or SDL2, this is thanks to the extensive design of HAL, our compatibility layer's API.

## CI/CD Pipiline

* We do CI for build sanity checks on every push to the repository using GitHub Actions.
* We currently don't have a way to do CD on the cloud due to a lack of code signing capability.
* However, the author has a release script and thus releases are fully automated on his MacBook Pro.
  * The release script builds all binaries and uploads them to the Web site and GitHub.
  * It also posts a message to the Discord server.
  * This is generally done in 10 minutes via FTTH, or 20 minutes via LTE/5G.

## Trivia

Did you know that...

* Midori wears a watermelon themed tie and pair of hair ribbons to stand out in her uniform?
* "Suika" means "watermelon" in Japanese?
* Suika2 is the successor to "Suika Studio" and the author is a pioneer in the field of GUI editors for visual novel creation?
  * [See the 2002 version here](https://github.com/ktabata/suika-studio-2002-gpl)
  * [See the 2003 version here](https://github.com/ktabata/suika-studio-2003-gpl)

## Sponsors

Here's where you raise your banner!

**Come forward**, those who resonate with our vision and are of like mind.

VENITE ET VIDEBITIS
