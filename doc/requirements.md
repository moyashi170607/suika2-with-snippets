***

# Software Requirements Specification for Suika2

Author: ktabata

Organization: the Suika2 Development Team

Update: 28th October 2022

Revision: 0.2

***

# Introduction

Suika2 is a tool for creating visual novels.
It is a cross-platform software, running on at least Windows, Mac, Linux, iOS, Android and Web.

This document is for engine developers, not for game developers.
It describes software requirements specification for Suika2, from aspect of software engineering.

## Terms

* Engine developers ... The Suika2 developers who create Suika2.
* Game developers ... The Suika2 users who create games.
* End users ... The users of games which are created with Suika2.

***

# Screen Components

Screen components are layered.
They consist of the following, in order from bottom to top:

1. Background
2. Back Center Character
3. Left Character
4. Right Character
5. Center Character
6. Message Box
7. Name Box
8. Character Face
9. Click Animation
10. Options
11. Collapsed System Menu / System Menu

## Background

The background image is the lowest layer that makes up the screen.
It must have the same size as window size.

## Characters

1. `center` (Center Character)
2. `right` (Right Character)
3. `left` (Left Character)
4. `back` (Back Center Character)
5. `face` (Character Face)

There are four types of regular characters,
depending on their horizontal display position
(`center`, `right`, `left` and `back).

The `center` and `back` have the same horizontal display position,
but the layers displayed are different.

There is a position for the character's `face` to be displayed to
the left of the message box.

All the characters are vertically aligned at the bottom of the screen.

## Message Box

The message box is a window for outputting text.

It has a "bg" and "fg" images.
Basically, the bg image is displayed.
When a developer creates "In-box Menu" and a game user pointed a button,
the button area in the fg image is displayed.

Since the fg image must be specified,
if developers do not use it,
they can simply specify the same file for the bg image.

### In-box Menu

Message box can have menu buttons.
Acceptable button types are:

1. `qsave`
2. `qload`
3. `save`
4. `load`
5. `history`
6. `auto`
7. `skip`
8. `config`
9. `hide`

The current demo game uses only the `hide` button.
If developers want to use the other buttons,
they can simply provide a message box images and add some configs.

## Namebox

The namebox is a window for outputting character's name.

Names are usually horizontally centered, but can be left-aligned by config.

The namebox can be hidden by a config
in order to realize the full screen style novel.

## Collapsed System Menu

This is a button that is visible during the display of messages and choices.
Clicking this button brings up the "System Menu".
The collapsed system menu is invisible and disabled during
System Menu" is visible.

## System Menu

This is a menu that consists of buttons.
The button types are the following:

1. `qsave`
2. `qload`
3. `save`
4. `load`
5. `history`
6. `auto`
7. `skip`
8. `config`

The system menu is activated by clicking on the collapsed system menu or
by right-clicking when a message or options are displayed.

While the system menu is displayed,
clicking anywhere other than the buttons or right-clicking will exit
the system menu and return to the collapsed system menu.

***

# Command Features

The important building block of Suika2 functionality is "command".
Therefore, more than half of the functional requirements are in the design of the commands.

See also the "Command Reference" for the details of command implementation.

## Showing a Background Image

This feature shows a background image.
The command that accomplishes this feature is `@bg`.

This feature fades in a new background over a specified time in second.
The originally displayed characters and background will disappear.

Developers can use the hex RGB color specifier (e.g., `#0088ff`)
instead of background image file name.

## Showing Character Images

This feature shows a character image.
The command that accomplishes this feature is `@ch`.

Character layers include `center` (center front), `right`, `left`,
`back` (center back) and `face` (character face).

All the characters are vertically aligned at the bottom of the screen.

## Showing Message

This feature shows the descriptive part in the story.
It is not an atmark command that makes this feature possible.
A message is one-line text that does not begin with an atmark,
asterisk or colon.

Messages are output to a message box.
The descriptive part does not have the speaker's name,
so the name box is hidden while the message is showing.

A messages is displayed one character at a time.
When clicked in the middle, all characters are displayed at once.
When all characters are displayed, Suika2 goes into
"Click Waiting Mode for Message" and displays a click animation for prompt.
If a click is made while a click animation is being displayed,
Suika2 moves to the next command.

At any point during the display of a message,
if the control key is pressed, Suika2 moves to the next command.

Game developers can use `\n` to insert new line.

Game developers can append the message to the previous one
by inserting '\' at the begenning of the message.
This is useful to create a full screen style novel.

End users can return key instead of left click.

## Showing a Line

This feature shows the dialog in the story.
It is not an atmark command that makes this feature possible.
A line is one-line text that begin with an asterisk.

A line consists of a name, a message and optionally a voice file name and
they are separated by asterisks. Namely:
```
*name*dialog message
*name*voice.ogg*dialog message
```

The name appears before the message.

Audio starts playing when the name is displayed.
Normally, audio playback is stopped before moving on to the next command,
but this behavior can be suppressed in the config.

Other functions are the same as "Showing Message".

## Playing Background Music

This feature plays background music.
The command that accomplishes this feature is `@bgm`.

Unless otherwise specified, BGM is played on a loop.
Game developers can use `once` specifier to prevent loop play.

There is no ability to crossfade multiple BGMs.
Therefore, game developers may need to first fade out the old BGM and then
fade in the new BGM.

## Playing Sound Effect

This feature plays sound effect.
The command that accomplishes this feature is `@se`.

## Fading a Volume

This feature changes the volume.
The command that accomplishes this feature is `@se`.

There are three audio tracks: `bgm`, `se` and `voice`.

The volume will fade for the specified time.
This feature does not wait until the fade in is complete.
Game developers need to combine with "Waiting for Specified Time" feature if necessary.

Local volumes are stored for each saved data.
Global volumes are common to all saved data.
The final volume will be a multiplication of local and global volumes.

## Showing Options

This feature shows options and lets user choose one option,
then jumps to the label selected.
The command that accomplishes this feature is `@choose`.

This feature can display up to eight options.

## Label

This is a jump target inside a script.
It is not an atmark command, but one-line of text that begin with a colon.

## Showing an Animation

This feature displays a character sprite in motion.
The command that accomplishes this feature is `@cha`.

This feature translates one character sprite at a time
and can also change its alpha value.

Constant velocity, acceleration and deceleration can be used for translation.

`@cha` is the abbreviation of `character animation`.

## Changing Characters and Background at Once

This feature changes character sprites and background image at once.
The command that accomplishes this feature is `@chs`.

It is also possible to change only the characters or only the background.

`@chs` is the abbreviation of `change stage`.

## Shaking the Screen

This feature shakes the screen.
The command that accomplishes this feature is `@shake`.

This feature allows the screen to oscillate horizontally or vertically.
The oscillation is a "simple harmonic motion".
Game developers can specify the period and the amplitude.

## Playing a Video

This feature plays a video file.
The command that accomplishes this feature is `@video`.

Available video formats vary from platform to platform,
but will be standardized to mp4 in the future.
See also "File Formats" section.

## Showing a GUI

This feature shows a graphical user interface (GUI).
GUI is a menu consisting of three images.

The command that accomplishes this feature is `@gui`.

## Waiting for a Click

This feature waits for a user click.
The command that accomplishes this feature is `@click`.

During auto mode, Suika2 resumes after a 2-second pause.
See also "Click Waiting Mode" section.

## Waiting for Specified Time

This feature waits for specified time in second.
The command that accomplishes this feature is `@wait`.

If a click or keystroke is made, the wait is canceled.
See also "Timed Waiting Mode"

## Prohibiting Skip

This feature enables/disables skip by control key.
The command that accomplishes this feature is `@skip`.

Typically, this feature is used to display a logo.

## Jumping to a Label

This feature moves the script execution position to the specified label.
The command that accomplishes this feature is `@jump`.

Currently, there is no way to jump directly to labels in other scripts.

## Setting a Variable

This feature sets variable value.
The command that accomplishes this feature is `@set`.

It can perform calculations as well as simple assignments.
Addition, subtraction, multiplication, remainder, and random number generation
are possible.

## Branching by Variable

This feature moves the script execution position to the specified label
if the specified condition of variable meets.
The command that accomplishes this feature is `@if`.

## Jumping to a Script

This feature loads a script file.
The command that accomplishes this feature is `@load`.

## Setting a Chapter Name

This feature sets a chapter name.
The command that accomplishes this feature is `@chapter`.

The name of the chapter is reflected in the window title and saved data items.

## Multilingualization

This feature automatically switches the language to be displayed
depending on the user's execution environment.

To use this feature, a line must be written that begins with a plus sign.

```
+en+Hello.
+fr+Bonjour.
```

This is not exactly a command, but it is a prefix to a command.

***

# Variables

All variables are 32-bit integers.

## Local Variables

Variable from `$0` to `$9999` are local variables which are stored in each
save data.
Typically, these are used to branch scenarios.

## Global Variables

Variables from `$10000` to `$10999` are global variables which are common to
all save data.
Typically, these are used to record which pictures should be displayed
in the gallery.

***

# Seen Flags

A seen flag is stored for each message.

Unseen messages cannot be skipped.
However, game developers can set an option to modify this behaviour.

The reason why the term "seen" is used instead of "read" is that
"read" appears to be an instruction to the computer.

Seen flags are stored in `sav` folder.
They are grouped into indivisual scripts.
The file name of the seen flags is a hex representation of
the script file name.

***

# Main Screen

Here, the screen during normal gameplay is referred to as the main screen.
See "Special Screens" for other screens.

The main screen has some modes.

## Background Changing Mode

This mode changes the background image.

During this mode, the message box is not displayed.

## Character Changing Mode

This mode changes the character images and the background image.

While `@ch` command changes one character at once,
`@chs` command changes 0 or more characters and optionally the background.

During this mode, the message box is not displayed.
However, game developers can optionally set a config to display the message box
during this mode.

## Text Output Mode for Message

In this mode, text is output one character at a time to the message box.

If the message has a character's name,
the name of the character is output to namebox prior to this mode.

When the text output is complete, Suika2 enters the "Click Waiting Mode for Message".

When a click or enter key is pressed during text output, Suika2 enters "Click Waiting Mode for Message".

When control key is pressed, Suika2 moves to the next command.

## Click Waiting Mode for Message

In this mode, a prompt animation appears and waits for the user to click.
If not in auto mode, this mode ends when there is a click or keystroke.
If in auto mode, this mode ends when the playback of the voice is completed
or the waiting time based on the number of characters is completed.

This mode is not executed when in skip mode and the message is already seen.

## Sound Fading Mode

This mode proceeds simultaneously with the other modes.
If necessary, game developers can use a timed wait for completion of the fading.

## Timed Waiting Mode

This mode waits for the specified seconds.
If a click or keystroke is made, the wait is canceled.

## Click Waiting Mode

This mode stops action until a user click or keystroke is made.
During auto mode, Suika2 resumes after a 2-second pause.

## Auto Mode

This mode proceeds simultaneously with the other modes.
It takes effect to "Click Waiting Mode for Message".
See also "Click Waiting Mode for Message".

During this mode, a banner representing the auto mode is displayed.

## Skip Mode

This mode proceeds simultaneously with the other modes.
It takes effect to "Click Waiting Mode for Message".
See also "Click Waiting Mode for Message".

During this mode, a banner representing the skip mode is displayed.

***

# Special Screens

Apart from the main screen (normal layered game screen),
there are some special screens.

These special screens are realized by the graphical user interface (GUI) feature.

## Save Screen

The save screen is for viewing and recording saved data.

This screen is realized by the GUI file `save.txt`

The number of save slots per page can be determined in save.txt.

## Load Screen

The load screen is for viewing and loading saved data.

This screen is realized by the GUI file `load.txt`

The number of save slots per page can be determined in load.txt.

## History Screen

The history screen displays the history of messages.
The maximum number of messages recorded is 100.

End users can click on a line with voice to play the audio.

This screen is realized by the GUI file `history.txt`

## Config Screen

The config screen allows end users to set volume, text speed,
auto mode speed and font.
It also provides "back to game title logo" button.
It may also show the preview of text speed and auto mode speed.

This screen is realized by the GUI file `config.txt`

***

# Graphical User Interface

Graphical user interface (GUI) interacts with user.
It is described by a text file, and it defines various buttons.

GUI is used to implement the special screen mode.
Typically, it is also used to implement the title menu and so on.

Note that in a GUI file,
the characters after `#` in a line are ignored as comments.

## GUI File Header

GUI file has a header.
In a header, developers have to define three images;
`idle`, `hover` and `active`.

```
global {
    idle:   config-idle.png;
    hover:  config-hover.png;
    active: config-active.png;
}
```

`idle` image is displayed as a base layer.
`hover` image is displayed when the button area is pointed.
`active` image is displayed when the button is active, or,
for slidebar buttons always displayed for a slidebar.

## Jump To Label Button

This button is used for creating normal menu.

```
QUIT {
    type: goto;
    label: QUIT;
    x: 960;
    y: 497;
    width: 317;
    height: 201;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

## Save/Load Page N Button

This button is used for pagenation of save/load screen.

```
PAGE1 {
    type: savepage;
    index: 0;
    x: 1137;
    y: 0;
    width: 70;
    height: 63;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

## Save Slot Button

This button is used for creating save slot.

```
SAVESLOT1 {
    type: save;
    index: 0;
    x: 50;
    y: 138;
    width: 1106;
    height: 140;
    margin: 10;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

When game developers create the save slot button,
they need an extra header item for the GUI file.

```
    saveslot: 3;
```

## Load Slot Button

This button is used for creating load slot.

```
SAVESLOT1 {
    type: load;
    index: 0;
    x: 50;
    y: 138;
    width: 1106;
    height: 140;
    margin: 10;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

When game developers create the load slot button,
they need an extra header item for the GUI file.

```
    saveslot: 3;
```

## Text Speed Slider

This button is used for creating text speed slide bar.

```
TEXTSPEED {
    type: textspeed;
    x: 68;
    y: 250;
    width: 266;
    height: 21;
    pointse: btn-change.ogg;
}
```

Note that:
* The knob for the slide bar is on the far left of the active image
* The width of the slide bar knob is equal to the height of the button

## Auto Mode Speed

This button is used for creating auto mode speed slide bar.

```
AUTOSPEED {
    type: autospeed;
    x: 68;
    y: 339;
    width: 266;
    height: 21;
    pointse: btn-change.ogg;    
}
```

Note that:
* The knob for the slide bar is on the far left of the active image
* The width of the slide bar knob is equal to the height of the button

##  Text Speed Preview

This button is used for displaying preview of text and auto mode speed.

```
PREVIEW {
    type: preview;
    msg: "This message is a preview of text speed and auto mode speed.";
    x: 442;
    y: 453;
    width: 590;
    height: 120;
}
```

## BGM Volume Slider

This button is used for setting BGM volume.

```
BGM {
    type: bgmvol;
    x: 420;
    y: 249;
    width: 266;
    height: 21;
    pointse: btn-change.ogg;
}
```

Note that:
* The knob for the slide bar is on the far left of the active image
* The width of the slide bar knob is equal to the height of the button

## Sound Effect Volume Slider

This button is used for setting sound effect volume.

```
SE {
    type: sevol;
    file: click.ogg;
    x: 420;
    y: 339;
    width: 266;
    height: 21;
    pointse: btn-change.ogg;
}
```

Note that:
* The knob for the slide bar is on the far left of the active image
* The width of the slide bar knob is equal to the height of the button

## Voice (All-character) Volume Slider

This button is used for setting voice (all-character) volume.

```
VOICE {
    type: voicevol;
    file: 025.ogg;
    x: 68;
    y: 498;
    width: 266;
    height: 21;
    pointse: btn-change.ogg;
}
```

Note that:
* The knob for the slide bar is on the far left of the active image
* The width of the slide bar knob is equal to the height of the button

## Voice (Per-character) Volume Slider

This button is used for setting voice (per-character) volume.

See also "Per-character Voice Volume Settings" section.

```
MIDORI {
    type: charactervol;
    index: 1;
    file: 025.ogg;
    x: 506;
    y: 506;
    width: 266;
    height: 21;
    pointse: btn-change.ogg;
}
```

Note that:
* The knob for the slide bar is on the far left of the active image
* The width of the slide bar knob is equal to the height of the button

## Font Selection Button

This button is used for setting font.

```
FONT1 {
    type: font;
    file: VL-PGothic-Regular.ttf;
    x: 770;
    y: 328;
    width: 87;
    height: 30;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

When there are multiple font buttons,
the active image is displayed for the selected one.

## Full Screen Button

This button is used for setting full screen mode.

```
FULLSCREEN {
    type: fullscreen;
    x: 750;
    y: 206;
    width: 200;
    height: 25;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

When there are full screen and window buttons,
the active image is displayed for the selected one.

## Window Button

This button is used for setting window mode.

```
WINDOW {
    type: window;
    x: 995;
    y: 206;
    width: 200;
    height: 25;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

When there are full screen and window buttons,
the active image is displayed for the selected one.

##  Reset Button

This button is used for resetting options.

```
DEFAULT {
    type: default;
    x: 1131;
    y: 61;
    width: 115;
    height: 40;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

## Page Button

This button is used for moving to other GUI.

```
PAGE2 {
    type: gui;
    file: system-page2.txt;
    x: 1234;
    y: 132;
    width: 35;
    height: 35;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

## Jump To File Button

This button is for jumping to a script.

```
TITLE {
    type: title;
    file: init.txt;
    x: 1007;
    y: 652;
    width: 109;
    height: 25;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

## Cancel Button

This button closes GUI screen.

```
BACK {
    type: cancel;
    x: 1156;
    y: 653;
    width: 103;
    height: 21;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

## Gallery Button

This button is visible when the specified variable is non-zero.
This feature is for the gallery mode.

```
CG1 {
    type: gallery;
    label: CG1;
    var: $10000;
    x: 1156;
    y: 653;
    width: 103;
    height: 21;
    pointse: btn-change.ogg;
    clickse: click.ogg;
}
```

***

# Configuration Features

Suika2 has a configuration file that allows customization of the application.
Configuration file consists of `key=value` lines.
Some keys are required and some are optional.

Note that configuration lines beginning with `#` are ignored as comments.

## Language Settings

Suika2 provides game developers only with Japanese and English error messages.
However, it provides multiple-language stories to end users.

### International Mode

If international mode is enabled, error messages will be in English. Otherwise, they will be in Japanese.
Turning on international mode also enables language mappings.

Adding `i18n=1` enables international mode.

### Language Mapping

This is the ability to display messages in a specific language
when a specific system locale is set.

For example, `language.ja=en` is an indication that the message is
to be displayed in English when the system locale is Japanese.

The default language mappings are as follows (falls back to English).
```
language.en=en
language.fr=en
language.de=en
language.es=en
language.it=en
language.el=en
language.ru=en
language.zh=en
language.tw=en
language.ja=en
language.other=en
```

## Window Settings

Here, a window is the drawable area of the screen.

### Window Title

This is the application title.

In a desktop operating system, a window may have a title.
In such an environment,
this application title becomes the first half of the window title.
The second half of the window title is the chapter name,
but this is omissible by a config.

```
window.title=Suika
```

### Window Width and Height

This is the window width and height.

```
window.width=1280
window.height=720
```

### Background Color

Game developers can choose whether the window background should be
white or black.

Game developers can choose white for pop games or black for chic games.

```
window.white=1
```

## Font Settings

### Font File Name

This is the font file name.
Font files are stored in `font` folder.

```
font.file=VL-PGothic-Regular.ttf
```

### Font Size

This is the font size in pixels.
Font size is applied to name, message, history and save item text.

```
font.size=30
```

### Font Colors

It is possible to specify the base color of the text and the outline color.

```
font.color.r=255
font.color.g=255
font.color.b=255
font.outline.color.r=128
font.outline.color.g=128
font.outline.color.b=128
```

### Font Outline

Game developers can choose whether or not to display font outlines.

```
font.outline.remove=0
```

## Namebox Settings

### Image

This is the image file name of namebox.
The file is stored in `cg` folder.

```
namebox.file=namebox.png
```

### Position

This is the position to show the namebox.

```
namebox.x=95
namebox.y=480
```

### Top Margin

This is the top margin of text in the namebox image.

```
namebox.margin.top=11
```

### Namebox Left Margin

This is the left margin of text in the namebox image.

### Namebox Centring

Game developers can choose whether or not to center the character name in the namebox.

```
namebox.centering.no=0
```

### Namebox Visibility

Game developers can choose whether or not to show namebox.

```
namebox.hidden=0
```

## Message Box Settings

### Message Box Images

There are background and foreground images.
Foreground image is used for in-box menu such as hide button.

### Message Box Position

This is a position to show the message box image.

```
msgbox.x=43
msgbox.y=503
```

### Message Box Margins

* Left margin of text in the message box image
* Top margin of text in the message box image
* Right margin of text in the message box image
* Line spacing

```
msgbox.margin.left=80
msgbox.margin.top=50
msgbox.margin.right=80
msgbox.margin.line=40
```

### Message Box Text Speed

Game developers can set the number of characters of text to be displayed per second.

```
msgbox.speed=40.0
```

### Position of Hide Button

Message box can have hide button.
This is optional.

```
msgbox.btn.hide.x=1146
msgbox.btn.hide.y=16
msgbox.btn.hide.width=29
msgbox.btn.hide.height=29
```

### Sound Effects

Message box has sound effects.

```
msgbox.btn.change.se=btn-change.ogg
msgbox.history.se=click.ogg
msgbox.config.se=click.ogg
msgbox.hide.se=click.ogg
msgbox.show.se=click.ogg
msgbox.auto.cancel.se=click.ogg
msgbox.skip.cancel.se=click.ogg
```

### Skipping Unseen Messages

The game developer can decide whether unread text can be skipped.
Skipping includes skip mode and control key skip.

## Click Animation Settings

The click-waiting prompt that appears above the message box is a click animation.

### Position

This is a position to show the click animation image.

```
click.x=1170
click.y=660
```

### Following The Text

Click animation is usually displayed at a fixed position,
but it can also be displayed at the end of the text.
This is useful when making a full screen style novel.

```
click.move=0
```

### Images

First one is required, and others are optional.

```
click.file1=click1.png
click.file2=click2.png
click.file3=click3.png
click.file4=click4.png
click.file5=click5.png
click.file6=click5.png
```

### Interval

This is the click animation interval in second.

```
click.interval=1.0
```

### Visibility

Developers can hide click animation.
This is mainly for the visually impaired.

```
click.interval=1.0
```

## Options Settings

### Images

There are background and foreground images for an option box.
Background image is displayed usually,
 but when the option box is pointed by mouse, foreground image is displayed.

```
switch.bg.file=switch-bg.png
switch.fg.file=switch-fg.png
```

### Position

This is the position of the first option box.

```
switch.x=406
switch.y=129
```

### Vertical Space

This is the vertical space between the option box images.

```
switch.margin.y=20
```

### Top Margin

This is the top margin of text in the option box image.

```
switch.text.margin.y=18
```

### Color

This is the color of pointed option's text.

```
switch.color.active=0
switch.color.active.body.r=255
switch.color.active.body.g=0
switch.color.active.body.b=0
switch.color.active.outline.r=128
switch.color.active.outline.g=128
switch.color.active.outline.b=128
```

### Sound Effects

Options can have sound effects.

```
switch.parent.click.se.file=click.ogg
switch.child.click.se.file=click.ogg
switch.change.se=btn-change.ogg
```

## Save/Load Screen Settings

Most of settings for save/load screens are defined in GUI file.
Here, we have just one configuration.

### Thumbnail Size

This is the thumbnail size of the save data.

```
save.data.thumb.width=213
save.data.thumb.height=120
```

## System Menu Settings

### Position

This is the position to show the system menu image.

```
sysmenu.x=731
sysmenu.y=29
```

### Images

System menu has three images; `idle`, `hover`, `disable`.
`idle` is the base image.
`hover` is displayed when the button area is pointed.
`disable` is displayed when the button is disabled.

```
sysmenu.idle.file=sysmenu-idle.png
sysmenu.hover.file=sysmenu-hover.png
sysmenu.disable.file=sysmenu-disable.png
```

### Button Positions

The following buttons are available:
* `qsave` (quick save)
* `qload` (quick load)
* `save` (save mode)
* `load` (save mode)
* `auto` (auto mode)
* `skip` (skip mode)
* `history` (history mode)
* `config` (config mode)

The following is the example for `qsave`.

```
sysmenu.qsave.x=62
sysmenu.qsave.y=7
sysmenu.qsave.width=60
sysmenu.qsave.height=58
```

### Sound Effects

System menu can have sound effects.

```
sysmenu.enter.se=click.ogg
sysmenu.leave.se=click.ogg
sysmenu.change.se=btn-change.ogg
sysmenu.qsave.se=click.ogg
sysmenu.qload.se=click.ogg
sysmenu.save.se=click.ogg
sysmenu.load.se=click.ogg
sysmenu.auto.se=click.ogg
sysmenu.skip.se=click.ogg
sysmenu.history.se=click.ogg
sysmenu.config.se=click.ogg
```

### Collapsed Position

This is the position to show the collapsed system menu image.
Collapsed system menu is usually displayed, and when it is clicked, (expanded) system menu is displayed.

```
sysmenu.collapsed.x=1219
sysmenu.collapsed.y=29
```

### Collapsed Images

Collapsed system menu has two images; `idle` and `hover`.

```
sysmenu.collapsed.idle.file=sysmenu-collapsed-idle.png
sysmenu.collapsed.hover.file=sysmenu-collapsed-hover.png
```

### Collapsed Sound Effect

Collapsed system menu can have sound effect.

```
sysmenu.collapsed.se=btn-change.ogg
```

### Visibility

Game developers can decide whether collapsed system menu and system menu are visible.

```
sysmenu.hidden=0
```

Note: This will take effect when displaying messages.
When displaying options, collapsed system menu or system menu are always shown,
because of lack of a way to save without the system menu.

## Auto Mode Settings

Suika2 shows auto mode banner when auto mode is enabled.

### Image

This is the banner image.

```
automode.banner.file=auto.png
```

### Position

This is the banner position.

```
automode.banner.x=0
automode.banner.y=126
```

### Speed

This is a speed of auto mode.
Wait time for the message is `automode.speed` seconds per character.

```
automode.speed=0.15
```

## Skip Mode Settings

Suika2 shows skip mode banner when skip mode is enabled.

### Image

This is the banner image.

```
skipmode.banner.file=skip.png
```

### Position

This is the banner position.

```
skipmode.banner.x=0
skipmode.banner.y=186
```

## Initial Sound Volumes

These are the sound volume values for the initial boot time.
If a user has the save data,
then the volume settings in the save data will be used
instead of the initial sound volumes.

```
sound.vol.bgm=1.0
sound.vol.voice=1.0
sound.vol.se=1.0
sound.vol.character=1.0
```

## Per-character Voice Volume Settings

If the character name of a line matches to the name list,
then the per-character volume will be applied.

Per-character volumes can be set in the config screen. (#0 to #15)
See also "Voice (Per-character) Volume Slider" section.

Developers can specify the names for up to 15 characters. (#1 to #15)

If the character name of the message doesn't match the name list,
then the #0 per-character volume will be used.

The following is the example of name list.
```
sound.character.name1=Midori
```

## Character Message Colors

Game developers can specify text colors for up to 64 characters.
Colors are defined for each character by name.

To use character message color, write the following.
```
serif.color1.name=Haruka
serif.color1.r=255
serif.color1.g=200
serif.color1.b=200
serif.color1.outline.r=0
serif.color1.outline.g=0
serif.color1.outline.b=0
```

## UI Messages

User interface messages can be localized.
However, they can not be multilingualized at this moment.

```
ui.msg.quit=Are you sure you want to quit?
ui.msg.title=Are you sure you want to go to title?
ui.msg.delete=Are you sure you want to delete the save data?
ui.msg.overwrite=Are you sure you want to overwrite the save data?
ui.msg.default=Are you sure you want to reset the settings?
```

## Miscellaneous

There are detailed config items according to individual user requirements.

### Voice Continues On Click

This option enables voice continuation on click.
Normally, voice playback is stopped before moving on to the next command,
but this option suppresses this behaviour.

See alose "Showing a Line" section.

To enable this, write the following line in the config:
```
voice.stop.off=1
```

### Full Screen Mode

This option disables full screen mode.
If game developers don't want to allow full screen mode,
write the following line.

```
window.fullscreen.disable=1
```

### Window Maximization

This option disables window maximization.
If game developers don't want to allow maximization of Window,
write the following line.
```
window.maximize.disable=1
```

### Window Title Separator

Window title consists of the application title and the chapter name.
This separator is added between the application title and the chapter name.

To set the separator, write the following line. Note that there is a space after `=`.
```
window.title.separator= 
```

### Chapter Name

Game developers can decide whether to show chapter name in window title.

To hide chapter name from window title, write the following setting.
```
window.title.chapter.disable=1
```

### Show Message Box On Character Change

By default, the message box disappears while characters are changing.
With this option, developers can decide whether to display message box
while character is changing.

To enable this option, write the following setting.
```
msgbox.show.on.ch=1
```

## Release Mode

This mode is used for installing games to the "Program Files" path on Windows.
If this configuration is enabled,
save data will be stored in OS-specific locations such as 'AppData' on Windows
and 'Library' on macOS.

To enable release mode, write the following setting.
```
release=1
```

## Note

Initial values for text speed and auto mode speed cannot be set.
The default value for these is `0.5`.

***

# Package

Package is an archive file which contains all game data except video files.
The package file name is `data01.arc`.

## Generation

To generate package file, developers can use `pack` program or `Suika2 Pro for Creators`.

## Obfuscation

Obfuscation key is stored in `key.h`, and developers can change the value for their games.

## Usage

To use package file, put it in the folder which contains Suika2 application (app folder).
Game data folders (e.g., `bg`, `ch`, `bgm` and etc.) must be excluded from the app folder.
If the game data folder exists in the app folder, Suika2 uses the files in the game data folder instead of the files in the package file.

***

# File Formats

## Texts

Script files, GUI files and config file are plain text file.

### Encoding

Text files must be encoded in UTF-8.

### New Line

CR+LF, LF and CR are accepted.
These can be mixed.

## Image

PNG and JPEG are supported.
Note that gray scaled JPEG is not supported.

## Audio

The only accepted format is Ogg Vorbis 44.1kHz stereo or monaural.

## Video

On Windows, `.wmv` is the recommended format.
AVI with H.264 and AAC will work on Windows 10 or later.

On macOS, `.mp4` with H.264 and AAC is the recommended format.
QuickTime (`.mov` or `.qt`) will work too.

On Linux, any format supported by Gstreamer will work.
Game developers must instruct end users to install neccesary Gstreamer plugin.

In the future, it is planned to move to `.mp4` with H.264 and AAC on all platforms.

***

# Non-functional Requirements

## Native Application

Suika2 binaries are applications that run natively on the target platform.
This is based on the idea of achieving the fastest possible speed of operation.

## Performance Requirements

### CPU Usage

Suika2 can only use CPU time within the range that the OS determines
Suika2's CPU utilization to be "low".

The evaluation is based on the 6th generation Core i7 and its built-in GPU.

### Fast Math

Suika2 needs to speed up vector operations as much as possible.

On Intel/AMD platforms, Suika2 support SSE, SSE2, SSE3, SSE4.1, SSE4.2, AVX and AVX2.
Suika2 automatically chooses the fastest extension.
Currently, AVX512 is not supported because of lack of test machine.

### GPU

On a desktop OS, Suika2 uses GPU acceleration when possible and otherwise
it falls back to 2D rendering because of the compatibility.

On other environments, Suika2 always uses GPU acceleration because
the CPU may be non-powerful enough.

Fallback without GPU acceleration will be removed in the future.

### 2D Rendering

Suika2 uses its own 2D rendering engine if GPU acceleration is not available.

In addition, offscreen renderings are not GPU accelerated and done by 2D rendering.

### Frame Rate

The frame rate is set as high as 60 fps,
within the range that the OS determines Suika2's CPU utilization to be "low".

On Windows and Linux, if the GPU acceleration is enabled,
the target frames rate is 60fps, and if not, 30fps.

On macOS, the target frame rate is 30fps.

On iOS and Android, the frame rate is determined by OpenGL View of the OS.