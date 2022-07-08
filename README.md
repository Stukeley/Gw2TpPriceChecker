# Gw2TpPriceChecker

<p align="center">
  <img src="https://2.bp.blogspot.com/-3fGtcONBdHY/XWVVLClPw0I/AAAAAAABafA/g2kjcEEsHzUrpvQcy1r4wSgXyLaRAAdpwCLcBGAs/s200/Evon-Gnashblade-Charr-Icon.jpg">
</p>

Simple tool to check an item's price on the Trading Post in Guild Wars 2, and send an alert if the price is below/above set threshold.

## User Interface

<p align="center">
  <img src="https://i.imgur.com/mVQ6X3T.png">
</p>

## How to use

1. The item to fetch prices for can be selected in two ways:
a) By ID - copy the item's Id (for example, from the wiki) and paste it in the Item ID box
b) By Name - type in the item's name or parts of it
Either way, the other value will be filled in automatically.

2. Select comparison type - a value from the drop-down list:
- empty (default value) - you will receive no alerts, but the price will still be checked every interval
- ">B" (buy price greater than) - you will receive alerts when the insta-buy price is **above** the specified value
- ">S" (sell price greater than) - you will receive alerts when the insta-sell price is **above** the specified value
- "<B" (buy price less than) - you will receive alerts when the insta-buy price is **below** the specified value
- "<S" (sell price less than) - you will receive alerts when the insta-sell price is **below** the specified value

Example: let's say we're monitoring Piece of Unidentified Gear (green). We input the value: 2s 32c. The current prices are: Buy Order 2s 36c, Sell order 2s 37c.
- If we selected Empty, the price will simply be updated every interval.
- If we selected ">B", we will get alerts when the Highest Buy Order is at 2s 33c or above. (this is already the case in the example, so we will get alerts instantly)
- If we selected ">S", we will get alerts when the Lowest Sell Order is at 2s 33c or above. (this is already the case in the example, so we will get alerts instantly)
- If we selected "<B", we will get alerts when the Highest Buy Order is at 2s 31c or below.
- If we selected "<S", we will get alerts when the Lowest Sell Order is at 2s 31c or below.

<p align="center">
  <img src="https://i.imgur.com/C05m0Vs.png">
</p>

3. Input the coin value - when you click (or otherwise select) the box below "Enable price alerts?", a second window will pop up, and you can specify the amount of gold, silver and copper there.

<p align="center">
  <img src="https://i.imgur.com/tRUYPNt.png">
</p>

The price on the main window is displayed in copper, and will automatically update once the second window is closed.

4. Optional - set interval - how often the price will be checked. Has to be a value between 30 and 300, default is 60.

5. Press Start to start the application - the first update will occur immediately, the next ones every interval. To stop checking prices, press Stop.

## Remarks
The item's prices are checked and updated every interval - the interval can be set by the user, but has to be a value between 30 and 300 (seconds).

The app has tooltips - if anything is unclear, move over the control for the tooltip to appear.

If you do not select a comparison type - that is, leave the empty space there - you will not receive any alerts. The price will still be updated.

When you type in the item's name, you don't have to type the exact name - the application uses [Levenshtein distance](https://www.wikiwand.com/en/Levenshtein_distance) to match closely related strings. Therefore, even if you type everything in lower case, omit symbols or some letters, you should get the correct item.

Examples:

- the bifrot -> The Bifrost
- antique summoning -> Antique Summoning Stone
- +9 agony infus -> +9 Agony Infusion

## Used API endpoints
- /v2/items - for matching item's id with its name, and to fetch item icons
- /v2/commerce/prices - for checking prices on the Trading Post

## Sources
Project icon & leading image courtesy of Ilona Iske - https://elonian-gallery.com/

Coin icons (gold, silver, copper) as well as other item icons belong to ArenaNet: https://www.guildwars2.com/.

## Feature Requests
Have an idea you would like to see in the app? Feel free to contact me, make an Issue on GitHub, or let me know otherwise.
