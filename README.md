EPGP
====

This repository contains a command line utility that helps automate common EPGP management stuff. It's designed to work with exports from in-game EPGP addons.

Remove Raiders with no EP
-------------------------

To remove raiders with no EP gained (alts, socials, etc), simply run:

```epgp.exe --file=roster.json --trim --output=out.json```

`out.json` will contain a JSON file with only raiders in it.