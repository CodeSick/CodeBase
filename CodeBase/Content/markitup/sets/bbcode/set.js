// ----------------------------------------------------------------------------
// markItUp!
// ----------------------------------------------------------------------------
// Copyright (C) 2008 Jay Salvat
// http://markitup.jaysalvat.com/
// ----------------------------------------------------------------------------
// BBCode tags example
// http://en.wikipedia.org/wiki/Bbcode
// ----------------------------------------------------------------------------
// Feel free to add more tags
// ----------------------------------------------------------------------------
mySettings = {
    previewParserPath: '', // path to your BBCode parser
    markupSet: [
		{ name: 'Bold', key: 'B', openWith: '[b]', closeWith: '[/b]' },
		{ name: 'Italic', key: 'I', openWith: '[i]', closeWith: '[/i]' },
		{ name: 'Underline', key: 'U', openWith: '[u]', closeWith: '[/u]' },
		{ separator: '---------------' },
		{ name: 'Picture', key: 'P', replaceWith: '[img][![Url]!][/img]' },
		{ name: 'Link', key: 'L', openWith: '[url=[![Url]!]]', closeWith: '[/url]', placeHolder: 'Your text to link here...' },
		{ separator: '---------------' },
		{
		    name: 'Size', key: 'S', openWith: '[size=[![Text size]!]]', closeWith: '[/size]',
		    dropMenu: [
			{ name: 'Big', openWith: '[size=200]', closeWith: '[/size]' },
			{ name: 'Normal', openWith: '[size=100]', closeWith: '[/size]' },
			{ name: 'Small', openWith: '[size=50]', closeWith: '[/size]' }
		    ]
		},
		{ separator: '---------------' },
		{ name: 'Bulleted list', openWith: '[list]\n', closeWith: '\n[/list]' },
		{ name: 'Numeric list', openWith: '[list=[![Starting number]!]]\n', closeWith: '\n[/list]' },
		{ name: 'List item', openWith: '[*] ' },
		{ separator: '---------------' },
		{ name: 'Quotes', openWith: '[quote]', closeWith: '[/quote]' },
		{ name: 'Code', openWith: '[code]', closeWith: '[/code]' },
		{ separator: '---------------' },
		{ name: 'Clean', className: "clean", replaceWith: function (markitup) { return markitup.selection.replace(/\[(.*?)\]/g, "") } },
		{ name: 'Preview', className: "preview", call: 'preview' }
    ]
}

    // mIu nameSpace to avoid conflict.
miu = {
save: function(markItUp) {
    data = markItUp.textarea.value;
    ok = confirm("Save the content?");
    if (!ok) {
        return false;
    }
    $.post(markItUp.root+"utils/quicksave/save.php", "data="+data, function(response) {
        if(response === "MIU:OK") {
            alert("Saved!");
        }
    }
        );
},
load: function(markItUp) {
    $.get(markItUp.root+"utils/quicksave/load.php", function(response) {
        if(response === "MIU:EMPTY") {
            alert("Nothing to load");
        } else {
            ok = confirm("Load the previously saved content?");
            if (!ok) {
                return false;
            }
            markItUp.textarea.value = response;
            alert("Loaded!");
        }
    }
        );
},        
// Deal with Tiny Url server-side Script
tinyUrl: function (markItUp) {
    var url, tinyUrl;
    url = prompt("Url:", "http://");
    if (url) {
        $.ajaxSetup( { async:false } );
        $.post(markItUp.root+"utils/tinyurl/get.php", "url="+url, function(content) {
            tinyUrl = content;    
        }
            );
    } else {
        tinyUrl = "";
    }
    return '<a href="'+tinyUrl+'"(!( title="[![Title]!]")!)>';
},
    
// Deal with Html Tidy server-side Script
tidyRepair: function(markItUp) {
    var tidy;
    if (markItUp.selection !== "") {
        $.ajax({
            async:   false,
            type:    "POST",
            url:     markItUp.root+"utils/htmltidy/repair.php",
            data:    "selection="+encodeURIComponent(markItUp.selection),
            success:function(content) {
                tidy = content;    
            }
        });
    } else {
        $.ajax({
            async:   true,
            type:    "POST",
            url:     markItUp.root+"utils/htmltidy/repair.php",
            data:    "data="+encodeURIComponent(markItUp.textarea.value),
            success:function(content) {
                tidy = content;    
                markItUp.textarea.value = tidy;
            }
        });
    }    
    return tidy;
},
    
// Deal with Html Tidy server-side Script
tidyReport: function(markItUp) {
    $.ajax({
        async:    false,
        type:     "POST",
        url:      markItUp.root+"utils/htmltidy/report.php",
        data:     "data="+encodeURIComponent(markItUp.textarea.value),
        success:function(content) {
            win = window.open("", "htmlTidyReport","width=600, height=400, resizable=yes, scrollbars=yes");
            win.document.open();
            win.document.write(content);
            win.document.close();
            win.focus();
        }
    });
},
    
// Deal with Rss Feed Grabber server-side Script
rssFeedGrabber: function(markItUp) {
    var feed, limit = 100;
    url = prompt('Rss Feed Url', 'http://rss.news.yahoo.com/rss/topstories');
    if (markItUp.altKey) {
        limit = prompt('Top stories', '5');
    }
    $.ajax({
        async:     false,
        type:     "POST",
        url:     markItUp.root+"utils/rssfeed/grab.php",
        data:    "url="+url+"&limit="+limit,
        success:function(content) {
            feed = content;
        }
    }
        );    
    if (feed == "MIU:ERROR") {
        alert("Can't find a valid RSS Feed at "+url);
        return false;
    }
    return feed;
}
}