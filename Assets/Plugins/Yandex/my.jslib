mergeInto(LibraryManager.library, {

	ShowAd: function (_data) 
	{
		var dataJson = UTF8ToString(_data);
		var stringData = JSON.parse(dataJson);	
		
		if (sdk !== null && typeof sdk !== 'undefined') 
		{
			sdk.adv.showFullscreenAdv({
				callbacks: {
					onOpen: function() {
						myGameInstance.SendMessage(stringData.ObjectName, stringData.MethodStartName);
					},
					onClose: function(wasShown) {
						myGameInstance.SendMessage(stringData.ObjectName, stringData.MethodEndName);
					},
					onError: function(error) {
						// some action on error
					}
				}
			})
		}
	},
	
	SaveExtern: function (_data) 
	{
		if (player !== null && typeof player !== 'undefined') 
		{
			var stringData = UTF8ToString(_data);
			var myObj = JSON.parse(stringData);
				
			player.setData(myObj);
		}
	},
	
	LoadExtern: function (_data)
	{
		var dataJson = UTF8ToString(_data);
		var stringData = JSON.parse(dataJson);
	
		if (player !== null && typeof player !== 'undefined') 
		{
			player.getData().then(function(_data)
			{
				const myJSON = JSON.stringify(_data);
				myGameInstance.SendMessage(stringData.ObjectName, stringData.MethodEndName, myJSON);
			});
		} else {
			myGameInstance.SendMessage(stringData.ObjectName, stringData.MethodErrorName);
		}
	},
	
	GetLang: function ()
	{
		if (sdk !== null && typeof sdk !== 'undefined') 
		{
			var lang = sdk.environment.i18n.lang;
			var bufferSize = lengthBytesUTF8(lang) + 1;
			var buffer = _malloc(bufferSize);
			stringToUTF8(lang, buffer, bufferSize);
			
			return buffer;
		}
	},
	
	GetTLD: function ()
	{
		if (sdk !== null && typeof sdk !== 'undefined') 
		{
			var tld = sdk.environment.i18n.tld;
			var bufferSize = lengthBytesUTF8(tld) + 1;
			var buffer = _malloc(bufferSize);
			stringToUTF8(tld, buffer, bufferSize);
			
			return buffer;
		}
	},
	
	RateGame: function ()
	{
		if (sdk !== null && typeof sdk !== 'undefined') 
		{
			sdk.feedback.canReview().then(function(response)
			{
				var value = response.value;
				var reason = response.reason;
				
				if (value) {
					sdk.feedback.requestReview().then(function(response) 
					{
						var feedbackSent = response.feedbackSent;
						console.log(feedbackSent);
					});
				} else {
					console.log(reason);
				}
			});
		}
	},
	
	SetLeaderboard: function (_data)
	{
		if (sdk !== null && typeof sdk !== 'undefined') 
		{
			if (sdk.isAvailableMethod('player.getIDsPerGame')) 
			{
				var dataJson = UTF8ToString(_data);
				var stringData = JSON.parse(dataJson);
			
				sdk.getLeaderboards().then(function(lb)
				{
					lb.setLeaderboardScore(stringData.BoardName, stringData.Value);
				});
			}
		}
	},
	
	GetLeaderboard: function (_data)
	{
		var dataJson = UTF8ToString(_data);
		var stringData = JSON.parse(dataJson);
		
		if (sdk !== null && typeof sdk !== 'undefined') 
		{
			sdk.getLeaderboards().then(function(lb)
			{
				lb.getLeaderboardEntries(stringData.BoardName, { quantityTop: 10 }).then(function(res)
				{
					const myJSON = JSON.stringify(res);
					myGameInstance.SendMessage(stringData.ObjectName, stringData.MethodEndName, myJSON);
				});
			});
		}
	},
	
	ShowReward: function (_data)
	{
		var dataJson = UTF8ToString(_data);
		var stringData = JSON.parse(dataJson);		
	
		if (sdk !== null && typeof sdk !== 'undefined') 
		{
			sdk.adv.showRewardedVideo({
				callbacks: {
					onOpen: function() {
					  console.log('Video ad open.');
					  
					  myGameInstance.SendMessage(stringData.ObjectName, stringData.MethodStartName);
					},
					onRewarded: function() {
					  console.log('Rewarded!');

					},
					onClose: function() {
					  console.log('Video ad closed.');
					  
					  myGameInstance.SendMessage(stringData.ObjectName, stringData.MethodEndName);
					}, 
					onError: function(e) {
					  console.log('Error while open video ad:', e);
					}
				}
			})
		}
	},
});