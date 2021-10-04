# Gw2TpPriceChecker

<p align="center">
  <img src="https://2.bp.blogspot.com/-Sup3fJ8XOwo/XYeIaosTIzI/AAAAAAABbAI/5WFwvlFP1q0osmR_QWs9vQ-eZhxodZ98QCLcBGAsYHQ/s200/Karma-Icon.jpg">
</p>

Simple tool to check an item's price on the Trading Post in Guild Wars 2, and send an alert if the price is below/above set threshold.

## User Interface

<p align="center">
  <img src="https://i.imgur.com/ZVj4e8U.png">
</p>

## Remarks
The item's prices are checked and updated every minute.

The app has tooltips - if anything is unclear, move over the control for the tooltip to appear.

If you do not select a comparison type - that is, leave the empty space there - you will not receive any alerts. The price will still be updated.

For now, to use the app you need to provide the item's ID - you can find an item's ID on the Wiki or other third-party websites.

## Used API endpoints
- /v2/items - for matching item's id with its name
- /v2/commerce/prices - for checking prices on the Trading Post

## Sources
Project icon & leading image courtesy of Ilona Iske - https://elonian-gallery.com/

Coin icons (gold, silver, copper) belong to ArenaNet
