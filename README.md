# Steam Marketeer

Simple command-line tool to get information about "language coverage" on any public Steam Store page.\
The coverage is calculated by realtime information in respect with [Steam Hardware & Software Survey](https://store.steampowered.com/hwsurvey/Steam-Hardware-Software-Survey-Welcome-to-Steam)

### Usage
`SteamMarketeer.exe APP_ID`
#### Options:
`APP_ID` - app id from Steam Store (you can see these numbers in any game url)

#### Examples:
🟢 Good situation _(100% coverage)_ 🟢 - [Sin Slayers: Reign of The 8th](https://store.steampowered.com/app/2790000/Sin_Slayers_Reign_of_The_8th/)
```
> SteamMarketeer.exe 2790000
...
Store page contains all languages, everything is okay!
```

🟡 Normal situation _(98% coverage)_ 🟡 - [Streat Fighter 6](https://store.steampowered.com/app/1364780/Street_Fighter_6)
```
> SteamMarketeer.exe 1364780
...
Missed: Bulgarian (bg)
Missed: Czech (cs)
Missed: Greek (el)
Missed: Hungarian (hu)
Missed: Indonesian (id)
Missed: Romanian (ro)
Missed: Thai (th)
Missed: Vietnamese (vn)
Store page does not contains all languages and covers only 98% of all users, please take a look at this!
```

🔴 Bad situation _(56% coverage)_ 🔴 - [Folk Hero](https://store.steampowered.com/app/2342150/Folk_Hero/)
```
> SteamMarketeer.exe 2342150
...
Missed: Bulgarian (bg)
Missed: Chinese Simplified (zh-CN)
Missed: Chinese Traditional (zh-TW)
Missed: Czech (cs)
Missed: Danish (da)
Missed: Dutch (nl)
Missed: Finnish (fi)
Missed: Greek (el)
Missed: Hungarian (hu)
Missed: Indonesian (id)
Missed: Japanese (ja)
Missed: Korean (ko)
Missed: Norwegian (no)
Missed: Polish (pl)
Missed: Portuguese (pt)
Missed: Romanian (ro)
Missed: Spanish-Latin America (es-419)
Missed: Swedish (sv)
Missed: Thai (th)
Missed: Turkish (tr)
Missed: Ukrainian (uk)
Missed: Vietnamese (vn)
Store page does not contains all languages and covers only 56% of all users, please take a look at this!
```

## Contribute
Contribution in any form is very welcome. Bugs, feature requests or feedback can be reported in form of Issues.
