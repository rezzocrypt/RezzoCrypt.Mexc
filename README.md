# RezzoCrypt.Mexc
Library for accessing the [Mexc Exchange](https://www.mexc.com/register?inviteCode=1PjWy) api

Simple using:

Create connector for Mexc
```
var connector = new MexcConnection(apiKey, apiSecret);
```
* connector.Account - Contains account balance
* connector.Exchange - Exchange operations for you
* connector.Data - Market data
* connector.Service - Service exchange methods

All methods are async and return `Task<T>`:

```
var account = await connector.Account.AccountInfoAsync();
var orders   = await connector.Exchange.OpenedOrdersAsync("BTCUSDT");
var klines   = await connector.Data.KlineAsync("BTCUSDT", start, end, interval: "1m");
var time     = await connector.Service.TimeAsync();
```