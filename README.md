# TradeOgre
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