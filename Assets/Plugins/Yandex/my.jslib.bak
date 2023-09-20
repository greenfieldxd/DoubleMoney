mergeInto(LibraryManager.library, {

	ShowAd: function () 
	{
		sdk.adv.showFullscreenAdv({
			callbacks: {
				onClose: function(wasShown) {
					// some action after close
				},
				onError: function(error) {
					// some action on error
				}
			}
		})
	},
	
	GetLang: function ()
	{
		var lang = sdk.environment.i18n.lang;
		var bufferSize = lengthBytesUTF8(lang) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(lang, buffer, bufferSize);
		
		return buffer;
	},
	
	GetTLD: function ()
	{
		var tld = sdk.environment.i18n.tld;
		var bufferSize = lengthBytesUTF8(tld) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(tld, buffer, bufferSize);
		
		return buffer;
	},
	
	LoadExtern: function ()
	{
		if (sdk.isAvailableMethod('player.getIDsPerGame')) 
		{
			if (player && typeof player.getData === 'function') 
			{
				player.getData().then(function(_data)
				{
					const myJSON = JSON.stringify(_data);
					myGameInstance.SendMessage('Yandex Loading', 'YandexLoadingData', myJSON);
				});
			} else 
			{
				myGameInstance.SendMessage('Yandex Loading', 'UpdateState');
			}
		} else 
		{
			myGameInstance.SendMessage('Yandex Loading', 'UpdateState');
		}
	},
	
	SaveExtern: function (_data) 
	{
		if (sdk.isAvailableMethod('player.getIDsPerGame')) 
		{
			if (player && typeof player.setData === 'function') 
			{
				var stringData = UTF8ToString(_data);
				var myObj = JSON.parse(stringData);
				
				player.setData(myObj);
			}
		}
	},
	
	RateGame: function ()
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
	},
	
	SetLeaderboard: function (_mode, _value)
	{
		if (sdk.isAvailableMethod('player.getIDsPerGame')) 
		{
			sdk.getLeaderboards().then(function(lb)
			{
				var _boardName = 'leader';
				//if (_mode == 1) { _boardName = 'leaderease'; } 
				//else if (_mode == 2) { _boardName = 'leadernormal'; } 
				//else if (_mode == 3) { _boardName = 'leaderhard'; }			
				
				lb.setLeaderboardScore(_boardName, _value);
			});
		}
	},
	
	GetLeaderboard: function (_mode)
	{
		sdk.getLeaderboards().then(function(lb)
		{
			var _boardName = 'leader';
			//if (_mode == 1) { _boardName = 'leaderease'; } 
			//else if (_mode == 2) { _boardName = 'leadernormal'; } 
			//else if (_mode == 3) { _boardName = 'leaderhard'; }			
			
			lb.getLeaderboardEntries(_boardName, { quantityTop: 5 }).then(function(res)
			{
				const myJSON = JSON.stringify(res);
				myGameInstance.SendMessage('Game Record', 'YandexLeaderboardData', myJSON);
			});
		});
	},
});