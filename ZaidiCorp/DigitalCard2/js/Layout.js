﻿! function (name, definition) {
    if (typeof module != 'undefined' && module.exports) module.exports = definition()
    else if (typeof define == 'function' && define.amd) define(name, definition)
    else this[name] = definition()
}('bowser', function () {
    var t = true

    function detect(ua) {
        function getFirstMatch(regex) {
            var match = ua.match(regex);
            return (match && match.length > 1 && match[1]) || '';
        }

        function getSecondMatch(regex) {
            var match = ua.match(regex);
            return (match && match.length > 1 && match[2]) || '';
        }
        var iosdevice = getFirstMatch(/(ipod|iphone|ipad)/i).toLowerCase(),
            likeAndroid = /like android/i.test(ua),
            android = !likeAndroid && /android/i.test(ua),
            nexusMobile = /nexus\s*[0-6]\s*/i.test(ua),
            nexusTablet = !nexusMobile && /nexus\s*[0-9]+/i.test(ua),
            chromeos = /CrOS/.test(ua),
            silk = /silk/i.test(ua),
            sailfish = /sailfish/i.test(ua),
            tizen = /tizen/i.test(ua),
            webos = /(web|hpw)os/i.test(ua),
            windowsphone = /windows phone/i.test(ua),
            windows = !windowsphone && /windows/i.test(ua),
            mac = !iosdevice && !silk && /macintosh/i.test(ua),
            linux = !android && !sailfish && !tizen && !webos && /linux/i.test(ua),
            edgeVersion = getFirstMatch(/edge\/(\d+(\.\d+)?)/i),
            versionIdentifier = getFirstMatch(/version\/(\d+(\.\d+)?)/i),
            tablet = /tablet/i.test(ua),
            mobile = !tablet && /[^-]mobi/i.test(ua),
            xbox = /xbox/i.test(ua),
            result
        if (/opera|opr|opios/i.test(ua)) {
            result = {
                name: 'Opera',
                opera: t,
                version: versionIdentifier || getFirstMatch(/(?:opera|opr|opios)[\s\/](\d+(\.\d+)?)/i)
            }
        } else if (/coast/i.test(ua)) {
            result = {
                name: 'Opera Coast',
                coast: t,
                version: versionIdentifier || getFirstMatch(/(?:coast)[\s\/](\d+(\.\d+)?)/i)
            }
        } else if (/yabrowser/i.test(ua)) {
            result = {
                name: 'Yandex Browser',
                yandexbrowser: t,
                version: versionIdentifier || getFirstMatch(/(?:yabrowser)[\s\/](\d+(\.\d+)?)/i)
            }
        } else if (/ucbrowser/i.test(ua)) {
            result = {
                name: 'UC Browser',
                ucbrowser: t,
                version: getFirstMatch(/(?:ucbrowser)[\s\/](\d+(?:\.\d+)+)/i)
            }
        } else if (/mxios/i.test(ua)) {
            result = {
                name: 'Maxthon',
                maxthon: t,
                version: getFirstMatch(/(?:mxios)[\s\/](\d+(?:\.\d+)+)/i)
            }
        } else if (/epiphany/i.test(ua)) {
            result = {
                name: 'Epiphany',
                epiphany: t,
                version: getFirstMatch(/(?:epiphany)[\s\/](\d+(?:\.\d+)+)/i)
            }
        } else if (/puffin/i.test(ua)) {
            result = {
                name: 'Puffin',
                puffin: t,
                version: getFirstMatch(/(?:puffin)[\s\/](\d+(?:\.\d+)?)/i)
            }
        } else if (/sleipnir/i.test(ua)) {
            result = {
                name: 'Sleipnir',
                sleipnir: t,
                version: getFirstMatch(/(?:sleipnir)[\s\/](\d+(?:\.\d+)+)/i)
            }
        } else if (/k-meleon/i.test(ua)) {
            result = {
                name: 'K-Meleon',
                kMeleon: t,
                version: getFirstMatch(/(?:k-meleon)[\s\/](\d+(?:\.\d+)+)/i)
            }
        } else if (windowsphone) {
            result = {
                name: 'Windows Phone',
                windowsphone: t
            }
            if (edgeVersion) {
                result.msedge = t
                result.version = edgeVersion
            } else {
                result.msie = t
                result.version = getFirstMatch(/iemobile\/(\d+(\.\d+)?)/i)
            }
        } else if (/msie|trident/i.test(ua)) {
            result = {
                name: 'Internet Explorer',
                msie: t,
                version: getFirstMatch(/(?:msie |rv:)(\d+(\.\d+)?)/i)
            }
        } else if (chromeos) {
            result = {
                name: 'Chrome',
                chromeos: t,
                chromeBook: t,
                chrome: t,
                version: getFirstMatch(/(?:chrome|crios|crmo)\/(\d+(\.\d+)?)/i)
            }
        } else if (/chrome.+? edge/i.test(ua)) {
            result = {
                name: 'Microsoft Edge',
                msedge: t,
                version: edgeVersion
            }
        } else if (/vivaldi/i.test(ua)) {
            result = {
                name: 'Vivaldi',
                vivaldi: t,
                version: getFirstMatch(/vivaldi\/(\d+(\.\d+)?)/i) || versionIdentifier
            }
        } else if (sailfish) {
            result = {
                name: 'Sailfish',
                sailfish: t,
                version: getFirstMatch(/sailfish\s?browser\/(\d+(\.\d+)?)/i)
            }
        } else if (/seamonkey\//i.test(ua)) {
            result = {
                name: 'SeaMonkey',
                seamonkey: t,
                version: getFirstMatch(/seamonkey\/(\d+(\.\d+)?)/i)
            }
        } else if (/firefox|iceweasel|fxios/i.test(ua)) {
            result = {
                name: 'Firefox',
                firefox: t,
                version: getFirstMatch(/(?:firefox|iceweasel|fxios)[ \/](\d+(\.\d+)?)/i)
            }
            if (/\((mobile|tablet);[^\)]*rv:[\d\.]+\)/i.test(ua)) {
                result.firefoxos = t
            }
        } else if (silk) {
            result = {
                name: 'Amazon Silk',
                silk: t,
                version: getFirstMatch(/silk\/(\d+(\.\d+)?)/i)
            }
        } else if (/phantom/i.test(ua)) {
            result = {
                name: 'PhantomJS',
                phantom: t,
                version: getFirstMatch(/phantomjs\/(\d+(\.\d+)?)/i)
            }
        } else if (/slimerjs/i.test(ua)) {
            result = {
                name: 'SlimerJS',
                slimer: t,
                version: getFirstMatch(/slimerjs\/(\d+(\.\d+)?)/i)
            }
        } else if (/blackberry|\bbb\d+/i.test(ua) || /rim\stablet/i.test(ua)) {
            result = {
                name: 'BlackBerry',
                blackberry: t,
                version: versionIdentifier || getFirstMatch(/blackberry[\d]+\/(\d+(\.\d+)?)/i)
            }
        } else if (webos) {
            result = {
                name: 'WebOS',
                webos: t,
                version: versionIdentifier || getFirstMatch(/w(?:eb)?osbrowser\/(\d+(\.\d+)?)/i)
            };
            if (/touchpad\//i.test(ua)) {
                result.touchpad = t;
            }
        } else if (/bada/i.test(ua)) {
            result = {
                name: 'Bada',
                bada: t,
                version: getFirstMatch(/dolfin\/(\d+(\.\d+)?)/i)
            };
        } else if (tizen) {
            result = {
                name: 'Tizen',
                tizen: t,
                version: getFirstMatch(/(?:tizen\s?)?browser\/(\d+(\.\d+)?)/i) || versionIdentifier
            };
        } else if (/qupzilla/i.test(ua)) {
            result = {
                name: 'QupZilla',
                qupzilla: t,
                version: getFirstMatch(/(?:qupzilla)[\s\/](\d+(?:\.\d+)+)/i) || versionIdentifier
            }
        } else if (/chromium/i.test(ua)) {
            result = {
                name: 'Chromium',
                chromium: t,
                version: getFirstMatch(/(?:chromium)[\s\/](\d+(?:\.\d+)?)/i) || versionIdentifier
            }
        } else if (/chrome|crios|crmo/i.test(ua)) {
            result = {
                name: 'Chrome',
                chrome: t,
                version: getFirstMatch(/(?:chrome|crios|crmo)\/(\d+(\.\d+)?)/i)
            }
        } else if (android) {
            result = {
                name: 'Android',
                version: versionIdentifier
            }
        } else if (/safari|applewebkit/i.test(ua)) {
            result = {
                name: 'Safari',
                safari: t
            }
            if (versionIdentifier) {
                result.version = versionIdentifier
            }
        } else if (iosdevice) {
            result = {
                name: iosdevice == 'iphone' ? 'iPhone' : iosdevice == 'ipad' ? 'iPad' : 'iPod'
            }
            if (versionIdentifier) {
                result.version = versionIdentifier
            }
        } else if (/googlebot/i.test(ua)) {
            result = {
                name: 'Googlebot',
                googlebot: t,
                version: getFirstMatch(/googlebot\/(\d+(\.\d+))/i) || versionIdentifier
            }
        } else {
            result = {
                name: getFirstMatch(/^(.*)\/(.*) /),
                version: getSecondMatch(/^(.*)\/(.*) /)
            };
        }
        if (!result.msedge && /(apple)?webkit/i.test(ua)) {
            if (/(apple)?webkit\/537\.36/i.test(ua)) {
                result.name = result.name || "Blink"
                result.blink = t
            } else {
                result.name = result.name || "Webkit"
                result.webkit = t
            }
            if (!result.version && versionIdentifier) {
                result.version = versionIdentifier
            }
        } else if (!result.opera && /gecko\//i.test(ua)) {
            result.name = result.name || "Gecko"
            result.gecko = t
            result.version = result.version || getFirstMatch(/gecko\/(\d+(\.\d+)?)/i)
        }
        if (!result.msedge && (android || result.silk)) {
            result.android = t
        } else if (iosdevice) {
            result[iosdevice] = t
            result.ios = t
        } else if (mac) {
            result.mac = t
        } else if (xbox) {
            result.xbox = t
        } else if (windows) {
            result.windows = t
        } else if (linux) {
            result.linux = t
        }
        var osVersion = '';
        if (result.windowsphone) {
            osVersion = getFirstMatch(/windows phone (?:os)?\s?(\d+(\.\d+)*)/i);
        } else if (iosdevice) {
            osVersion = getFirstMatch(/os (\d+([_\s]\d+)*) like mac os x/i);
            osVersion = osVersion.replace(/[_\s]/g, '.');
        } else if (android) {
            osVersion = getFirstMatch(/android[ \/-](\d+(\.\d+)*)/i);
        } else if (result.webos) {
            osVersion = getFirstMatch(/(?:web|hpw)os\/(\d+(\.\d+)*)/i);
        } else if (result.blackberry) {
            osVersion = getFirstMatch(/rim\stablet\sos\s(\d+(\.\d+)*)/i);
        } else if (result.bada) {
            osVersion = getFirstMatch(/bada\/(\d+(\.\d+)*)/i);
        } else if (result.tizen) {
            osVersion = getFirstMatch(/tizen[\/\s](\d+(\.\d+)*)/i);
        }
        if (osVersion) {
            result.osversion = osVersion;
        }
        var osMajorVersion = osVersion.split('.')[0];
        if (tablet || nexusTablet || iosdevice == 'ipad' || (android && (osMajorVersion == 3 || (osMajorVersion >= 4 && !mobile))) || result.silk) {
            result.tablet = t
        } else if (mobile || iosdevice == 'iphone' || iosdevice == 'ipod' || android || nexusMobile || result.blackberry || result.webos || result.bada) {
            result.mobile = t
        }
        if (result.msedge || (result.msie && result.version >= 10) || (result.yandexbrowser && result.version >= 15) || (result.vivaldi && result.version >= 1.0) || (result.chrome && result.version >= 20) || (result.firefox && result.version >= 20.0) || (result.safari && result.version >= 6) || (result.opera && result.version >= 10.0) || (result.ios && result.osversion && result.osversion.split(".")[0] >= 6) || (result.blackberry && result.version >= 10.1) || (result.chromium && result.version >= 20)) {
            result.a = t;
        } else if ((result.msie && result.version < 10) || (result.chrome && result.version < 20) || (result.firefox && result.version < 20.0) || (result.safari && result.version < 6) || (result.opera && result.version < 10.0) || (result.ios && result.osversion && result.osversion.split(".")[0] < 6) || (result.chromium && result.version < 20)) {
            result.c = t
        } else result.x = t
        return result
    }
    var bowser = detect(typeof navigator !== 'undefined' ? navigator.userAgent : '')
    bowser.test = function (browserList) {
        for (var i = 0; i < browserList.length; ++i) {
            var browserItem = browserList[i];
            if (typeof browserItem === 'string') {
                if (browserItem in bowser) {
                    return true;
                }
            }
        }
        return false;
    }

    function getVersionPrecision(version) {
        return version.split(".").length;
    }

    function map(arr, iterator) {
        var result = [],
            i;
        if (Array.prototype.map) {
            return Array.prototype.map.call(arr, iterator);
        }
        for (i = 0; i < arr.length; i++) {
            result.push(iterator(arr[i]));
        }
        return result;
    }

    function compareVersions(versions) {
        var precision = Math.max(getVersionPrecision(versions[0]), getVersionPrecision(versions[1]));
        var chunks = map(versions, function (version) {
            var delta = precision - getVersionPrecision(version);
            version = version + new Array(delta + 1).join(".0");
            return map(version.split("."), function (chunk) {
                return new Array(20 - chunk.length).join("0") + chunk;
            }).reverse();
        });
        while (--precision >= 0) {
            if (chunks[0][precision] > chunks[1][precision]) {
                return 1;
            } else if (chunks[0][precision] === chunks[1][precision]) {
                if (precision === 0) {
                    return 0;
                }
            } else {
                return -1;
            }
        }
    }

    function isUnsupportedBrowser(minVersions, strictMode, ua) {
        var _bowser = bowser;
        if (typeof strictMode === 'string') {
            ua = strictMode;
            strictMode = void (0);
        }
        if (strictMode === void (0)) {
            strictMode = false;
        }
        if (ua) {
            _bowser = detect(ua);
        }
        var version = "" + _bowser.version;
        for (var browser in minVersions) {
            if (minVersions.hasOwnProperty(browser)) {
                if (_bowser[browser]) {
                    return compareVersions([version, minVersions[browser]]) < 0;
                }
            }
        }
        return strictMode;
    }

    function check(minVersions, strictMode, ua) {
        return !isUnsupportedBrowser(minVersions, strictMode, ua);
    }
    bowser.isUnsupportedBrowser = isUnsupportedBrowser;
    bowser.compareVersions = compareVersions;
    bowser.check = check;
    bowser._detect = detect;
    return bowser
});
(function ($) {
    UABBTrigger = {
        triggerHook: function (hook, args) {
            $('body').trigger('uabb-trigger.' + hook, args);
        },
        addHook: function (hook, callback) {
            $('body').on('uabb-trigger.' + hook, callback);
        },
        removeHook: function (hook, callback) {
            $('body').off('uabb-trigger.' + hook, callback);
        },
    };
})(jQuery);
jQuery(document).ready(function ($) {
    if (typeof bowser !== 'undefined' && bowser !== null) {
        var uabb_browser = bowser.name,
            uabb_browser_v = bowser.version,
            uabb_browser_class = uabb_browser.replace(/\s+/g, '-').toLowerCase(),
            uabb_browser_v_class = uabb_browser_class + parseInt(uabb_browser_v);
        $('html').addClass(uabb_browser_class).addClass(uabb_browser_v_class);
    }
    $('.uabb-row-separator').parents('.fl-builder').css('overflow-x', 'hidden');
    $('.uabb-row-separator').parents('.fl-builder').css('overflow-y', 'visible');
});
var wpAjaxUrl = 'https://vcard.makemyvcard.com/wp-admin/admin-ajax.php';
var flBuilderUrl = 'https://vcard.makemyvcard.com/wp-content/plugins/bb-plugin/';
var FLBuilderLayoutConfig = {
    anchorLinkAnimations: {
        duration: 1000,
        easing: 'swing',
        offset: 100
    },
    paths: {
        pluginUrl: 'https://vcard.makemyvcard.com/wp-content/plugins/bb-plugin/',
        wpAjaxUrl: 'https://vcard.makemyvcard.com/wp-admin/admin-ajax.php'
    },
    breakpoints: {
        small: 768,
        medium: 992
    },
    waypoint: {
        offset: 80
    }
};
(function ($) {
    if (typeof FLBuilderLayout != 'undefined') {
        return;
    }
    FLBuilderLayout = {
        init: function () {
            FLBuilderLayout._destroy();
            FLBuilderLayout._initClasses();
            FLBuilderLayout._initBackgrounds();
            if (0 === $('.fl-builder-edit').length) {
                FLBuilderLayout._initAnchorLinks();
                FLBuilderLayout._initHash();
                FLBuilderLayout._initModuleAnimations();
                FLBuilderLayout._initForms();
            }
        },
        refreshGalleries: function (element) {
            var $element = 'undefined' == typeof element ? $('body') : $(element),
                mfContent = $element.find('.fl-mosaicflow-content'),
                wmContent = $element.find('.fl-gallery'),
                mfObject = null;
            if (mfContent) {
                mfObject = mfContent.data('mosaicflow');
                if (mfObject) {
                    mfObject.columns = $([]);
                    mfObject.columnsHeights = [];
                    mfContent.data('mosaicflow', mfObject);
                    mfContent.mosaicflow('refill');
                }
            }
            if (wmContent) {
                wmContent.trigger('refreshWookmark');
            }
        },
        refreshGridLayout: function (element) {
            var $element = 'undefined' == typeof element ? $('body') : $(element),
                msnryContent = $element.find('.masonry');
            if (msnryContent.length) {
                msnryContent.masonry('layout');
            }
        },
        reloadSlider: function (element) {
            var $element = 'undefined' == typeof element ? $('body') : $(element),
                bxContent = $element.find('.bx-viewport > div').eq(0),
                bxObject = null;
            if (bxContent.length) {
                bxObject = bxContent.data('bxSlider');
                if (bxObject) {
                    bxObject.reloadSlider();
                }
            }
        },
        resizeAudio: function (element) {
            var $element = 'undefined' == typeof element ? $('body') : $(element),
                audioPlayers = $element.find('.wp-audio-shortcode.mejs-audio'),
                player = null,
                mejsPlayer = null,
                rail = null,
                railWidth = 400;
            if (audioPlayers.length && typeof mejs !== 'undefined') {
                audioPlayers.each(function () {
                    player = $(this);
                    mejsPlayer = mejs.players[player.attr('id')];
                    rail = player.find('.mejs-controls .mejs-time-rail');
                    var innerMejs = player.find('.mejs-inner'),
                        total = player.find('.mejs-controls .mejs-time-total');
                    if (typeof mejsPlayer !== 'undefined') {
                        railWidth = Math.ceil(player.width() * 0.8);
                        if (innerMejs.length) {
                            rail.css('width', railWidth + 'px!important');
                            mejsPlayer.options.autosizeProgress = true;
                            setTimeout(function () {
                                mejsPlayer.setControlsSize();
                            }, 50);
                            player.find('.mejs-inner').css({
                                visibility: 'visible',
                                height: 'inherit'
                            });
                        }
                    }
                });
            }
        },
        preloadAudio: function (element) {
            var $element = 'undefined' == typeof element ? $('body') : $(element),
                contentWrap = $element.closest('.fl-accordion-item'),
                audioPlayers = $element.find('.wp-audio-shortcode.mejs-audio');
            if (!contentWrap.hasClass('fl-accordion-item-active') && audioPlayers.find('.mejs-inner').length) {
                audioPlayers.find('.mejs-inner').css({
                    visibility: 'hidden',
                    height: 0
                });
            }
        },
        resizeSlideshow: function () {
            if (typeof YUI !== 'undefined') {
                YUI().use('node-event-simulate', function (Y) {
                    Y.one(window).simulate("resize");
                });
            }
        },
        _destroy: function () {
            var win = $(window);
            win.off('scroll.fl-bg-parallax');
            win.off('resize.fl-bg-video');
        },
        _isTouch: function () {
            if (('ontouchstart' in window) || (window.DocumentTouch && document instanceof DocumentTouch)) {
                return true;
            }
            return false;
        },
        _isMobile: function () {
            return /Mobile|Android|Silk\/|Kindle|BlackBerry|Opera Mini|Opera Mobi|webOS/i.test(navigator.userAgent);
        },
        _initClasses: function () {
            var body = $('body'),
                ua = navigator.userAgent;
            if (!body.hasClass('archive') && $('.fl-builder-content-primary').length > 0) {
                body.addClass('fl-builder');
            }
            if (FLBuilderLayout._isTouch()) {
                body.addClass('fl-builder-touch');
            }
            if (FLBuilderLayout._isMobile()) {
                body.addClass('fl-builder-mobile');
            }
            if (ua.indexOf('Trident/7.0') > -1 && ua.indexOf('rv:11.0') > -1) {
                body.addClass('fl-builder-ie-11');
            }
        },
        _initBackgrounds: function () {
            var win = $(window);
            if ($('.fl-row-bg-parallax').length > 0 && !FLBuilderLayout._isMobile()) {
                FLBuilderLayout._scrollParallaxBackgrounds();
                FLBuilderLayout._initParallaxBackgrounds();
                win.on('scroll.fl-bg-parallax', FLBuilderLayout._scrollParallaxBackgrounds);
            }
            if ($('.fl-bg-video').length > 0) {
                FLBuilderLayout._initBgVideos();
                FLBuilderLayout._resizeBgVideos();
                win.on('resize.fl-bg-video', FLBuilderLayout._resizeBgVideos);
            }
        },
        _initParallaxBackgrounds: function () {
            $('.fl-row-bg-parallax').each(FLBuilderLayout._initParallaxBackground);
        },
        _initParallaxBackground: function () {
            var row = $(this),
                content = row.find('> .fl-row-content-wrap'),
                src = row.data('parallax-image'),
                loaded = row.data('parallax-loaded'),
                img = new Image();
            if (loaded) {
                return;
            } else if (typeof src != 'undefined') {
                $(img).on('load', function () {
                    content.css('background-image', 'url(' + src + ')');
                    row.data('parallax-loaded', true);
                });
                img.src = src;
            }
        },
        _scrollParallaxBackgrounds: function () {
            $('.fl-row-bg-parallax').each(FLBuilderLayout._scrollParallaxBackground);
        },
        _scrollParallaxBackground: function () {
            var win = $(window),
                row = $(this),
                content = row.find('> .fl-row-content-wrap'),
                speed = row.data('parallax-speed'),
                offset = content.offset(),
                yPos = -((win.scrollTop() - offset.top) / speed);
            content.css('background-position', 'center ' + yPos + 'px');
        },
        _initBgVideos: function () {
            $('.fl-bg-video').each(FLBuilderLayout._initBgVideo);
        },
        _initBgVideo: function () {
            var wrap = $(this),
                width = wrap.data('width'),
                height = wrap.data('height'),
                mp4 = wrap.data('mp4'),
                youtube = wrap.data('youtube'),
                vimeo = wrap.data('vimeo'),
                mp4Type = wrap.data('mp4-type'),
                webm = wrap.data('webm'),
                webmType = wrap.data('webm-type'),
                fallback = wrap.data('fallback'),
                loaded = wrap.data('loaded'),
                fallbackTag = '',
                videoTag = null,
                mp4Tag = null,
                webmTag = null;
            if (loaded) {
                return;
            }
            videoTag = $('<video autoplay loop muted playsinline></video>');
            if ('undefined' != typeof fallback && '' != fallback) {
                videoTag.attr('poster', 'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7')
                videoTag.css('background', 'transparent url("' + fallback + '") no-repeat center center')
                videoTag.css('background-size', 'cover')
                videoTag.css('height', '100%')
            }
            if ('undefined' != typeof mp4 && '' != mp4) {
                mp4Tag = $('<source />');
                mp4Tag.attr('src', mp4);
                mp4Tag.attr('type', mp4Type);
                videoTag.append(mp4Tag);
            }
            if ('undefined' != typeof webm && '' != webm) {
                webmTag = $('<source />');
                webmTag.attr('src', webm);
                webmTag.attr('type', webmType);
                videoTag.append(webmTag);
            }
            if ('undefined' != typeof youtube && !FLBuilderLayout._isMobile()) {
                FLBuilderLayout._initYoutubeBgVideo.apply(this);
            } else if ('undefined' != typeof vimeo && !FLBuilderLayout._isMobile()) {
                FLBuilderLayout._initVimeoBgVideo.apply(this);
            } else {
                wrap.append(videoTag);
            }
            wrap.data('loaded', true);
        },
        _initYoutubeBgVideo: function () {
            var playerWrap = $(this),
                videoId = playerWrap.data('video-id'),
                videoPlayer = playerWrap.find('.fl-bg-video-player'),
                enableAudio = playerWrap.data('enable-audio'),
                startTime = 'undefined' !== typeof playerWrap.data('t') ? playerWrap.data('t') : 0,
                loop = 'undefined' !== typeof playerWrap.data('loop') ? playerWrap.data('loop') : 1,
                player;
            if (videoId) {
                FLBuilderLayout._onYoutubeApiReady(function (YT) {
                    setTimeout(function () {
                        player = new YT.Player(videoPlayer[0], {
                            videoId: videoId,
                            events: {
                                onReady: function (event) {
                                    if ("no" === enableAudio) {
                                        event.target.mute();
                                    } else if ("yes" === enableAudio && event.target.isMuted) {
                                        event.target.unMute();
                                    }
                                    playerWrap.data('YTPlayer', player);
                                    FLBuilderLayout._resizeYoutubeBgVideo.apply(playerWrap);
                                    event.target.playVideo();
                                },
                                onStateChange: function (event) {
                                    if (event.data === YT.PlayerState.ENDED && 1 === loop) {
                                        player.seekTo(startTime);
                                    }
                                },
                                onError: function (event) {
                                    console.info('YT Error: ' + event.data)
                                    FLBuilderLayout._onErrorYoutubeVimeo(playerWrap)
                                }
                            },
                            playerVars: {
                                controls: 0,
                                showinfo: 0,
                                rel: 0,
                                start: startTime,
                                loop: loop,
                                playlist: 1 === loop ? videoId : '',
                            }
                        });
                    }, 1);
                });
            }
        },
        _onErrorYoutubeVimeo: function (playerWrap) {
            fallback = playerWrap.data('fallback') || false
            if (!fallback) {
                return false;
            }
            playerWrap.find('iframe').remove()
            fallbackTag = $('<div></div>');
            fallbackTag.addClass('fl-bg-video-fallback');
            fallbackTag.css('background-image', 'url(' + playerWrap.data('fallback') + ')');
            playerWrap.append(fallbackTag);
        },
        _onYoutubeApiReady: function (callback) {
            if (window.YT && YT.loaded) {
                callback(YT);
            } else {
                setTimeout(function () {
                    FLBuilderLayout._onYoutubeApiReady(callback);
                }, 350);
            }
        },
        _initVimeoBgVideo: function () {
            var playerWrap = $(this),
                videoId = playerWrap.data('video-id'),
                videoPlayer = playerWrap.find('.fl-bg-video-player'),
                enableAudio = playerWrap.data('enable-audio'),
                player, width = playerWrap.outerWidth();
            if (typeof Vimeo !== 'undefined' && videoId) {
                player = new Vimeo.Player(videoPlayer[0], {
                    id: videoId,
                    loop: true,
                    title: false,
                    portrait: false,
                    background: true,
                    autopause: false
                });
                playerWrap.data('VMPlayer', player);
                if ("no" === enableAudio) {
                    player.setVolume(0);
                } else if ("yes" === enableAudio) {
                    player.setVolume(1);
                }
                player.play().catch(function (error) {
                    FLBuilderLayout._onErrorYoutubeVimeo(playerWrap)
                });
            }
        },
        _videoBgSourceError: function (e) {
            var source = $(e.target),
                wrap = source.closest('.fl-bg-video'),
                vid = wrap.find('video'),
                fallback = wrap.data('fallback'),
                fallbackTag = '';
            source.remove();
            if (vid.find('source').length) {
                return;
            } else if ('' !== fallback) {
                fallbackTag = $('<div></div>');
                fallbackTag.addClass('fl-bg-video-fallback');
                fallbackTag.css('background-image', 'url(' + fallback + ')');
                wrap.append(fallbackTag);
                vid.remove();
            }
        },
        _resizeBgVideos: function () {
            $('.fl-bg-video').each(function () {
                FLBuilderLayout._resizeBgVideo.apply(this);
                if ($(this).parent().find('img').length > 0) {
                    $(this).parent().imagesLoaded($.proxy(FLBuilderLayout._resizeBgVideo, this));
                }
            });
        },
        _resizeBgVideo: function () {
            if (0 === $(this).find('video').length && 0 === $(this).find('iframe').length) {
                return;
            }
            var wrap = $(this),
                wrapHeight = wrap.outerHeight(),
                wrapWidth = wrap.outerWidth(),
                vid = wrap.find('video'),
                vidHeight = wrap.data('height'),
                vidWidth = wrap.data('width'),
                newWidth = wrapWidth,
                newHeight = Math.round(vidHeight * wrapWidth / vidWidth),
                newLeft = 0,
                newTop = 0,
                iframe = wrap.find('iframe');
            if (vid.length) {
                if (vidHeight === '' || typeof vidHeight === 'undefined' || vidWidth === '' || typeof vidWidth === 'undefined') {
                    vid.css({
                        'left': '0px',
                        'top': '0px',
                        'width': newWidth + 'px'
                    });
                    vid.on('loadedmetadata', FLBuilderLayout._resizeOnLoadedMeta);
                } else {
                    if (newHeight < wrapHeight) {
                        newHeight = wrapHeight;
                        newWidth = Math.round(vidWidth * wrapHeight / vidHeight);
                        newLeft = -((newWidth - wrapWidth) / 2);
                    } else {
                        newTop = -((newHeight - wrapHeight) / 2);
                    }
                    vid.css({
                        'left': newLeft + 'px',
                        'top': newTop + 'px',
                        'height': newHeight + 'px',
                        'width': newWidth + 'px'
                    });
                }
            } else if (iframe.length) {
                if (typeof wrap.data('youtube') !== 'undefined') {
                    FLBuilderLayout._resizeYoutubeBgVideo.apply(this);
                }
            }
        },
        _resizeOnLoadedMeta: function () {
            var video = $(this),
                wrapHeight = video.parent().outerHeight(),
                wrapWidth = video.parent().outerWidth(),
                vidWidth = video[0].videoWidth,
                vidHeight = video[0].videoHeight,
                newHeight = Math.round(vidHeight * wrapWidth / vidWidth),
                newWidth = wrapWidth,
                newLeft = 0,
                newTop = 0;
            if (newHeight < wrapHeight) {
                newHeight = wrapHeight;
                newWidth = Math.round(vidWidth * wrapHeight / vidHeight);
                newLeft = -((newWidth - wrapWidth) / 2);
            } else {
                newTop = -((newHeight - wrapHeight) / 2);
            }
            video.parent().data('width', vidWidth);
            video.parent().data('height', vidHeight);
            video.css({
                'left': newLeft + 'px',
                'top': newTop + 'px',
                'width': newWidth + 'px',
                'height': newHeight + 'px'
            });
        },
        _resizeYoutubeBgVideo: function () {
            var wrap = $(this),
                wrapWidth = wrap.outerWidth(),
                wrapHeight = wrap.outerHeight(),
                player = wrap.data('YTPlayer'),
                video = player ? player.getIframe() : null,
                aspectRatioSetting = '16:9',
                aspectRatioArray = aspectRatioSetting.split(':'),
                aspectRatio = aspectRatioArray[0] / aspectRatioArray[1],
                ratioWidth = wrapWidth / aspectRatio,
                ratioHeight = wrapHeight * aspectRatio,
                isWidthFixed = wrapWidth / wrapHeight > aspectRatio,
                width = isWidthFixed ? wrapWidth : ratioHeight,
                height = isWidthFixed ? ratioWidth : wrapHeight;
            if (video) {
                $(video).width(width).height(height);
            }
        },
        _initModuleAnimations: function () {
            if (typeof jQuery.fn.waypoint !== 'undefined' && !FLBuilderLayout._isMobile()) {
                $('.fl-animation').each(function () {
                    var node = $(this),
                        nodeTop = node.offset().top,
                        winHeight = $(window).height(),
                        bodyHeight = $('body').height(),
                        waypoint = FLBuilderLayoutConfig.waypoint,
                        offset = '80%';
                    if (typeof waypoint.offset !== undefined) {
                        offset = FLBuilderLayoutConfig.waypoint.offset + '%';
                    }
                    if (bodyHeight - nodeTop < winHeight * 0.2) {
                        offset = '100%';
                    }
                    node.waypoint({
                        offset: offset,
                        handler: FLBuilderLayout._doModuleAnimation
                    });
                });
            }
        },
        _doModuleAnimation: function () {
            var module = 'undefined' == typeof this.element ? $(this) : $(this.element),
                delay = parseFloat(module.data('animation-delay'));
            if (!isNaN(delay) && delay > 0) {
                setTimeout(function () {
                    module.addClass('fl-animated');
                }, delay * 1000);
            } else {
                module.addClass('fl-animated');
            }
        },
        _initHash: function () {
            var hash = window.location.hash.replace('#', '').split('/').shift(),
                element = null,
                tabs = null,
                responsiveLabel = null,
                tabIndex = null,
                label = null;
            if ('' !== hash) {
                try {
                    element = $('#' + hash);
                    if (element.length > 0) {
                        if (element.hasClass('fl-accordion-item')) {
                            setTimeout(function () {
                                element.find('.fl-accordion-button').trigger('click');
                            }, 100);
                        }
                        if (element.hasClass('fl-tabs-panel')) {
                            setTimeout(function () {
                                tabs = element.closest('.fl-tabs');
                                responsiveLabel = element.find('.fl-tabs-panel-label');
                                tabIndex = responsiveLabel.data('index');
                                label = tabs.find('.fl-tabs-labels .fl-tabs-label[data-index=' + tabIndex + ']');
                                if (responsiveLabel.is(':visible')) {
                                    responsiveLabel.trigger('click');
                                } else {
                                    label[0].click();
                                    FLBuilderLayout._scrollToElement(element);
                                }
                            }, 100);
                        }
                    }
                } catch (e) { }
            }
        },
        _initAnchorLinks: function () {
            $('a').each(FLBuilderLayout._initAnchorLink);
        },
        _initAnchorLink: function () {
            var link = $(this),
                href = link.attr('href'),
                loc = window.location,
                id = null,
                element = null;
            if ('undefined' != typeof href && href.indexOf('#') > -1 && link.closest('svg').length < 1) {
                if (loc.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && loc.hostname == this.hostname) {
                    try {
                        id = href.split('#').pop();
                        if (!id) {
                            return;
                        }
                        element = $('#' + id);
                        if (element.length > 0) {
                            if (link.hasClass('fl-scroll-link') || element.hasClass('fl-row') || element.hasClass('fl-col') || element.hasClass('fl-module')) {
                                $(link).on('click', FLBuilderLayout._scrollToElementOnLinkClick);
                            }
                            if (element.hasClass('fl-accordion-item')) {
                                $(link).on('click', FLBuilderLayout._scrollToAccordionOnLinkClick);
                            }
                            if (element.hasClass('fl-tabs-panel')) {
                                $(link).on('click', FLBuilderLayout._scrollToTabOnLinkClick);
                            }
                        }
                    } catch (e) { }
                }
            }
        },
        _scrollToElementOnLinkClick: function (e, callback) {
            var element = $('#' + $(this).attr('href').split('#').pop());
            FLBuilderLayout._scrollToElement(element, callback);
            e.preventDefault();
        },
        _scrollToElement: function (element, callback) {
            var config = FLBuilderLayoutConfig.anchorLinkAnimations,
                dest = 0,
                win = $(window),
                doc = $(document);
            if (element.length > 0) {
                if (element.offset().top > doc.height() - win.height()) {
                    dest = doc.height() - win.height();
                } else {
                    dest = element.offset().top - config.offset;
                }
                $('html, body').animate({
                    scrollTop: dest
                }, config.duration, config.easing, function () {
                    if ('undefined' != typeof callback) {
                        callback();
                    }
                    if (undefined != element.attr('id')) {
                        if (history.pushState) {
                            history.pushState(null, null, '#' + element.attr('id'));
                        } else {
                            window.location.hash = element.attr('id');
                        }
                    }
                });
            }
        },
        _scrollToAccordionOnLinkClick: function (e) {
            var element = $('#' + $(this).attr('href').split('#').pop());
            if (element.length > 0) {
                var callback = function () {
                    if (element) {
                        element.find('.fl-accordion-button').trigger('click');
                        element = false;
                    }
                };
                FLBuilderLayout._scrollToElementOnLinkClick.call(this, e, callback);
            }
        },
        _scrollToTabOnLinkClick: function (e) {
            var element = $('#' + $(this).attr('href').split('#').pop()),
                tabs = null,
                label = null,
                responsiveLabel = null;
            if (element.length > 0) {
                tabs = element.closest('.fl-tabs');
                responsiveLabel = element.find('.fl-tabs-panel-label');
                tabIndex = responsiveLabel.data('index');
                label = tabs.find('.fl-tabs-labels .fl-tabs-label[data-index=' + tabIndex + ']');
                if (responsiveLabel.is(':visible')) {
                    var callback = function () {
                        if (element) {
                            responsiveLabel.trigger('click');
                            element = false;
                        }
                    };
                    FLBuilderLayout._scrollToElementOnLinkClick.call(this, e, callback);
                } else {
                    label[0].click();
                    FLBuilderLayout._scrollToElement(element);
                }
                e.preventDefault();
            }
        },
        _initForms: function () {
            if (!FLBuilderLayout._hasPlaceholderSupport) {
                $('.fl-form-field input').each(FLBuilderLayout._initFormFieldPlaceholderFallback);
            }
            $('.fl-form-field input').on('focus', FLBuilderLayout._clearFormFieldError);
        },
        _hasPlaceholderSupport: function () {
            var input = document.createElement('input');
            return 'undefined' != input.placeholder;
        },
        _initFormFieldPlaceholderFallback: function () {
            var field = $(this),
                val = field.val(),
                placeholder = field.attr('placeholder');
            if ('undefined' != placeholder && '' === val) {
                field.val(placeholder);
                field.on('focus', FLBuilderLayout._hideFormFieldPlaceholderFallback);
                field.on('blur', FLBuilderLayout._showFormFieldPlaceholderFallback);
            }
        },
        _hideFormFieldPlaceholderFallback: function () {
            var field = $(this),
                val = field.val(),
                placeholder = field.attr('placeholder');
            if (val == placeholder) {
                field.val('');
            }
        },
        _showFormFieldPlaceholderFallback: function () {
            var field = $(this),
                val = field.val(),
                placeholder = field.attr('placeholder');
            if ('' === val) {
                field.val(placeholder);
            }
        },
        _clearFormFieldError: function () {
            var field = $(this);
            field.removeClass('fl-form-error');
            field.siblings('.fl-form-error-message').hide();
        }
    };
    $(function () {
        FLBuilderLayout.init();
    });
})(jQuery);
jQuery(document).ready(function ($) {
    if (!$('html').hasClass('fl-builder-edit')) {
        $('.uabb-modal-parent-wrapper').each(function () {
            $(this).appendTo(document.body);
        });
    }
});
(function ($) {
    UABBModalPopup = function (settings) {
        this.settings = settings;
        this.node = settings.id;
        this.modal_on = settings.modal_on;
        this.modal_custom = settings.modal_custom;
        this.modal_content = settings.modal_content;
        this.video_autoplay = settings.video_autoplay;
        this.enable_cookies = settings.enable_cookies;
        this.expire_cookie = settings.expire_cookie;
        this.esc_keypress = settings.esc_keypress;
        this.overlay_click = settings.overlay_click;
        this.responsive_display = settings.responsive_display;
        this.medium_device = settings.medium_device;
        this.small_device = settings.small_device;
        this._initModalPopup();
        var modal_resize = this;
        $(window).resize(function () {
            modal_resize._centerModal();
            modal_resize._resizeModalPopup();
        });
    };
    UABBModalPopup.prototype = {
        settings: {},
        node: '',
        modal_trigger: '',
        overlay: '',
        modal_popup: '',
        modal_on: '',
        modal_custom: '',
        modal_content: '',
        enable_cookies: '',
        expire_cookie: '',
        esc_keypress: '',
        overlay_click: '',
        video_autoplay: 'no',
        responsive_display: '',
        medium_device: '',
        small_device: '',
        _initModalPopup: function () {
            $this = this;
            $node_module = $('.fl-node-' + $this.node);
            $popup_id = $('.uamodal-' + $this.node);
            if (($('html').hasClass('uabb-active-live-preview') || !$('html').hasClass('fl-builder-edit')) && this.modal_on == 'custom' && this.modal_custom != '') {
                var custom_wrap = $(this.modal_custom);
                if (custom_wrap.length) {
                    custom_wrap.addClass("uabb-modal-action uabb-trigger");
                    var data_modal = 'modal-' + this.node;
                    custom_wrap.attr('data-modal', data_modal);
                    $this.modal_trigger = custom_wrap;
                    $this.modal_popup = $('#modal-' + $this.node);
                    var modal_trigger = custom_wrap,
                        modal_close = $popup_id.find('.uabb-modal-close'),
                        modal_popup = $('#modal-' + $this.node);
                    modal_trigger.bind("click", function () {
                        return false;
                    });
                    modal_trigger.on("click", $.proxy($this._showModalPopup, $this));
                    modal_close.on("click", $.proxy($this._removeModalHandler, $this));
                    $popup_id.find('.uabb-modal').on("click", function (e) {
                        if (e.target == this) {
                            modal_close.trigger("click");
                        }
                    });
                }
            } else if (this.modal_on == 'automatic') {
                this.modal_popup = $('#modal-' + this.node);
                var refresh_cookies_name = 'refresh-modal-' + this.node,
                    cookies_status = this.enable_cookies;
                if (cookies_status != 1 && Cookies.get(refresh_cookies_name) == 'true') {
                    Cookies.remove(refresh_cookies_name);
                }
            }
            this.overlay = $popup_id.find('.uabb-overlay');
            $node_module.find('.uabb-trigger').each(function (index) {
                $this.modal_trigger = $(this);
                $this.modal_popup = $('#modal-' + $this.node);
                var modal_trigger = $(this),
                    modal_close = $popup_id.find('.uabb-modal-close'),
                    modal_popup = $('#modal-' + $this.node);
                modal_trigger.bind("click", function () {
                    return false;
                });
                modal_trigger.on("click", $.proxy($this._showModalPopup, $this));
                modal_close.on("click", $.proxy($this._removeModalHandler, $this))
                $popup_id.find('.uabb-modal').on("click", function (e) {
                    if (e.target == this) {
                        modal_close.trigger("click");
                    }
                });
            });
            //this._centerModal();
            this._iphonecursorfix();
        },
        _showAutomaticModalPopup: function () {
            if (!this._isShowModal()) {
                return;
            }
            var cookies_name = 'modal-' + this.node,
                refresh_cookies_name = 'refresh-modal-' + this.node,
                cookies_status = this.enable_cookies,
                show_modal = true;
            if (cookies_status == 1) {
                if (Cookies.get(cookies_name) == 'true') {
                    show_modal = false;
                }
            } else {
                if (Cookies.get(refresh_cookies_name) == 'true') {
                    show_modal = false;
                }
                if (Cookies.get(cookies_name) == 'true') {
                    Cookies.remove(cookies_name);
                }
            }
            if (show_modal == true) {
                var parent_wrap = $('.fl-node-' + this.node),
                    popup_wrap = $('.uamodal-' + this.node),
                    trigger_args = '.uamodal-' + this.node + ' .uabb-modal-content-data',
                    close = popup_wrap.find('.uabb-modal-close'),
                    cookies_days = parseInt($this.expire_cookie),
                    current_this = this;
                if (popup_wrap.find('.uabb-content').outerHeight() > $(window).height()) {
                    $('html').addClass('uabb-html-modal');
                    popup_wrap.find('.uabb-modal').addClass('uabb-modal-scroll');
                }
                var modal = this.modal_popup;
                if (this.modal_content == 'youtube' || this.modal_content == 'vimeo') {
                    setTimeout(function () {
                        modal.addClass('uabb-show');
                    }, 300);
                } else {
                    modal.addClass('uabb-show');
                }
                this._videoAutoplay();
                if (this.esc_keypress == 1) {
                    $(document).on('keyup.uabb-modal', function (e) {
                        if (e.keyCode == 27) {
                            current_this.modal_popup.removeClass('uabb-show');
                            current_this._stopVideo();
                            $(document).unbind('keyup.uabb-modal');
                            if (cookies_status == 1) {
                                Cookies.set(cookies_name, 'true', {
                                    expires: cookies_days
                                });
                            } else {
                                Cookies.set(refresh_cookies_name, 'true');
                            }
                            UABBTrigger.triggerHook('uabb-modal-after-close', popup_wrap);
                        }
                    });
                }
                if (this.overlay_click == 1) {
                    this.overlay.on('click', function (ev) {
                        current_this.modal_popup.removeClass('uabb-show');
                        current_this._stopVideo();
                        if (cookies_status == 1) {
                            Cookies.set(cookies_name, 'true', {
                                expires: cookies_days
                            });
                        } else {
                            Cookies.set(refresh_cookies_name, 'true');
                        }
                        UABBTrigger.triggerHook('uabb-modal-after-close', popup_wrap);
                    });
                }
                close.on('click', function (ev) {
                    ev.preventDefault();
                    current_this.modal_popup.removeClass('uabb-show');
                    current_this._stopVideo();
                    if (popup_wrap.find('.uabb-content').outerHeight() > $(window).height()) {
                        setTimeout(function () {
                            $('html').removeClass('uabb-html-modal');
                            popup_wrap.find('.uabb-modal').removeClass('uabb-modal-scroll');
                        }, 300);
                    }
                    if (cookies_status == 1) {
                        Cookies.set(cookies_name, 'true', {
                            expires: cookies_days
                        });
                    } else {
                        Cookies.set(refresh_cookies_name, 'true');
                    }
                    UABBTrigger.triggerHook('uabb-modal-after-close', popup_wrap);
                });
                inner_content_close = popup_wrap.find('.uabb-close-modal');
                if (inner_content_close.length) {
                    inner_content_close.on('click', function () {
                        current_this.modal_popup.removeClass('uabb-show');
                        current_this._stopVideo();
                        if (cookies_status == 1) {
                            Cookies.set(cookies_name, 'true', {
                                expires: cookies_days
                            });
                        } else {
                            Cookies.set(refresh_cookies_name, 'true');
                        }
                        UABBTrigger.triggerHook('uabb-modal-after-close', popup_wrap);
                    });
                }
                UABBTrigger.triggerHook('uabb-modal-click', trigger_args);
            }
        },
        _showModalPopup: function () {
            if ($('html').hasClass('fl-builder-edit') && !$('html').hasClass('uabb-active-live-preview')) {
                return;
            }
            if (!this._isShowModal()) {
                return;
            }
            this._videoAutoplay();
            var active_modal = $('.fl-node-' + this.node),
                active_popup = $('.uamodal-' + this.node),
                trigger_args = '.uamodal-' + this.node + ' .uabb-modal-content-data';
            if (active_popup.find('.uabb-content').outerHeight() > $(window).height()) {
                $('html').addClass('uabb-html-modal');
                active_popup.find('.uabb-modal').addClass('uabb-modal-scroll');
            }
            var modal = $('#modal-' + this.node);
            if (this.modal_content == 'youtube' || this.modal_content == 'vimeo') {
                setTimeout(function () {
                    modal.addClass('uabb-show');
                }, 300);
            } else {
                modal.addClass('uabb-show');
            }
            if (this.overlay_click == 1) {
                this.overlay.on('click', $.proxy(this._removeModalHandler, this));
            }
            current_this = this;
            if (this.modal_trigger.hasClass('uabb-setperspective')) {
                setTimeout(function () {
                    current_this.modal_trigger.addClass('uabb-perspective');
                }, 25);
            }
            if (this.esc_keypress == 1) {
                $(document).on('keyup.uabb-modal', function (e) {
                    if (e.keyCode == 27) {
                        current_this._removeModalHandler();
                    }
                });
            }
            inner_content_close = active_popup.find('.uabb-close-modal');
            if (inner_content_close.length) {
                inner_content_close.on('click', $.proxy(this._removeModalHandler, this));
            }
            UABBTrigger.triggerHook('uabb-modal-click', trigger_args);
        },
        _removeModal: function (hasPerspective) {
            var active_modal = $('.fl-node-' + this.node),
                active_popup = $('.uamodal-' + this.node);
            this.modal_popup.removeClass('uabb-show');
            this._stopVideo();
            if (hasPerspective) {
                this.modal_trigger.removeClass('uabb-perspective');
            }
            setTimeout(function () {
                $('html').removeClass('uabb-html-modal');
                active_popup.find('.uabb-modal').removeClass('uabb-modal-scroll');
            }, 300);
            $(document).unbind('keyup.uabb-modal');
            UABBTrigger.triggerHook('uabb-modal-after-close', active_popup);
        },
        _removeModalHandler: function (ev) {
            this._removeModal(this.modal_trigger.hasClass('uabb-setperspective'));
        },
        _resizeModalPopup: function () {
            var active_modal = $('.fl-node-' + this.node),
                active_popup = $('.uamodal-' + this.node);
            if (active_popup.find('.uabb-modal').hasClass('uabb-show')) {
                if (active_popup.find('.uabb-content').outerHeight() > $(window).height()) {
                    $('html').addClass('uabb-html-modal');
                    active_popup.find('.uabb-modal').addClass('uabb-modal-scroll');
                } else {
                    $('html').removeClass('uabb-html-modal');
                    active_popup.find('.uabb-modal').removeClass('uabb-modal-scroll');
                }
            }
        },
        _videoAutoplay: function () {
            var active_modal = $('.fl-node-' + this.node),
                active_popup = $('.uamodal-' + this.node);
            if (this.video_autoplay == 'yes' && (this.modal_content == 'youtube' || this.modal_content == 'vimeo')) {
                var modal_iframe = active_popup.find('iframe'),
                    modal_src = modal_iframe.attr("src") + '&autoplay=1';
                modal_iframe.attr("src", modal_src);
            }
        },
        _stopVideo: function () {
            var active_modal = $('.fl-node-' + this.node),
                active_popup = $('.uamodal-' + this.node);
            if (this.modal_content != 'photo') {
                var modal_iframe = active_popup.find('iframe'),
                    modal_video_tag = active_popup.find('video');
                if (modal_iframe.length) {
                    var modal_src = modal_iframe.attr("src").replace("&autoplay=1", "");
                    modal_iframe.attr("src", '');
                    modal_iframe.attr("src", modal_src);
                } else if (modal_video_tag.length) {
                    modal_video_tag[0].pause();
                    modal_video_tag[0].currentTime = 0;
                }
            }
        },
        _isShowModal: function () {
            if (this.responsive_display != '') {
                var current_window_size = $(window).width(),
                    medium_device = parseInt(this.medium_device),
                    small_device = parseInt(this.small_device);
                if (this.responsive_display == 'desktop' && current_window_size > medium_device) {
                    return true;
                } else if (this.responsive_display == 'desktop-medium' && current_window_size > small_device) {
                    return true;
                } else if (this.responsive_display == 'medium' && current_window_size < medium_device && current_window_size > small_device) {
                    return true;
                } else if (this.responsive_display == 'medium-mobile' && current_window_size < medium_device) {
                    return true;
                } else if (this.responsive_display == 'mobile' && current_window_size < small_device) {
                    return true;
                } else {
                    return false;
                }
            }
            return true;
        },
        //_centerModal: function () {
        //    $this = this;
        //    popup_wrap = $('.uamodal-' + this.node);
        //    modal_popup = '#modal-' + $this.node;
        //    node = '.uamodal-' + $this.node;
        //    if ($('#modal-' + this.node).hasClass('uabb-center-modal')) {
        //        $('#modal-' + this.node).removeClass('uabb-center-modal');
        //    }
        //    if ($('#modal-' + this.node + '.uabb-show').outerHeight() != null) {
        //        var top_pos = (($(window).height() - $('#modal-' + this.node + '.uabb-show').outerHeight()) / 2);
        //        if (popup_wrap.find('.uabb-content').outerHeight() > $(window).height()) {
        //            $(node).find(modal_popup).css('top', '0');
        //            $(node).find(modal_popup).css('transform', 'none');
        //        } else {
        //            $(node).find(modal_popup).css('top', +top_pos + 'px');
        //            $(node).find(modal_popup).css('transform', 'none');
        //        }
        //    } else {
        //        if (popup_wrap.find('.uabb-content').outerHeight() > $(window).height()) {
        //            $(node).find(modal_popup).css('top', '0');
        //            $(node).find(modal_popup).css('transform', 'none');
        //        } else {
        //            $(node).find(modal_popup).css('top', '50%');
        //            $(node).find(modal_popup).css('transform', 'translateY(-50%)');
        //        }
        //    }
        //},
        _iphonecursorfix: function () {
            $this = this;
            popup_wrap = $('.uamodal-' + this.node);
            modal_popup = '#modal-' + $this.node;
            node = '.uamodal-' + $this.node;
            iphone = ((navigator.userAgent.match(/iPhone/i) == 'iPhone') ? 'iphone' : '');
            ipod = ((navigator.userAgent.match(/iPod/i) == 'iPod') ? 'ipod' : '');
            jQuery('html').addClass(iphone).addClass(ipod);
            jQuery('html.iphone .uabb-modal-action-wrap .uabb-module-content .uabb-button.uabb-trigger, html.ipod .uabb-modal-action-wrap .uabb-module-content .uabb-button.uabb-trigger').click(function () {
                jQuery('body').css('position', 'fixed');
            });
            if (this.overlay_click == 1) {
                jQuery(document).on('click', '.uabb-overlay', function () {
                    if (jQuery('html').hasClass('iphone') || jQuery('html').hasClass('ipod')) {
                        jQuery('body').css('position', 'relative');
                    }
                });
            }
            jQuery(document).on('click', '.uabb-modal-close', function () {
                if (jQuery('html').hasClass('iphone') || jQuery('html').hasClass('ipod')) {
                    jQuery('body').css('position', 'relative');
                }
            });
        }
    }
})(jQuery);
jQuery(document).ready(function ($) {
    if ('function' == typeof UABBModalPopup) {
        var UABBModalPopup_5ab3c640cc85e = new UABBModalPopup({
            id: '5ab3c640cc85e',
            modal_on: 'button',
            modal_custom: '',
            modal_content: 'content',
            video_autoplay: 'no',
            enable_cookies: '0',
            expire_cookie: '30',
            esc_keypress: '1',
            overlay_click: '1',
            responsive_display: '',
            medium_device: '992',
            small_device: '768',
        });
    }
});
jQuery(document).ready(function ($) {
    if ('function' == typeof UABBModalPopup) {
        var UABBModalPopup_5ab3c640cc893 = new UABBModalPopup({
            id: '5ab3c640cc893',
            modal_on: 'button',
            modal_custom: '',
            modal_content: 'photo',
            video_autoplay: 'no',
            enable_cookies: '0',
            expire_cookie: '30',
            esc_keypress: '1',
            overlay_click: '1',
            responsive_display: '',
            medium_device: '992',
            small_device: '768',
        });
    }
});
(function ($) {
    var form = $('.fl-builder-settings'),
        gradient_type = form.find('input[name=uabb_row_gradient_type]');
    $(document).on('change', 'input[name=uabb_row_radial_advance_options], input[name=uabb_row_linear_advance_options], input[name=uabb_row_gradient_type], select[name=bg_type]', function () {
        var form = $('.fl-builder-settings'),
            background_type = form.find('select[name=bg_type]').val(),
            linear_direction = form.find('select[name=uabb_row_uabb_direction]').val(),
            linear_advance_option = form.find('input[name=uabb_row_linear_advance_options]:checked').val(),
            radial_advance_option = form.find('input[name=uabb_row_radial_advance_options]:checked').val(),
            gradient_type = form.find('input[name=uabb_row_gradient_type]:checked').val();
        if (background_type == 'uabb_gradient') {
            if (gradient_type == 'radial') {
                setTimeout(function () {
                    form.find('#fl-field-uabb_row_linear_direction').hide();
                    form.find('#fl-field-uabb_row_linear_gradient_primary_loc').hide();
                    form.find('#fl-field-uabb_row_linear_gradient_secondary_loc').hide();
                }, 1);
                if (radial_advance_option == 'yes') {
                    form.find('#fl-field-uabb_row_radial_gradient_primary_loc').show();
                    form.find('#fl-field-uabb_row_radial_gradient_secondary_loc').show();
                }
            }
            if (gradient_type == 'linear') {
                setTimeout(function () {
                    form.find('#fl-field-uabb_row_radial_gradient_primary_loc').hide();
                    form.find('#fl-field-uabb_row_radial_gradient_secondary_loc').hide();
                }, 1);
                if (linear_direction == 'custom') {
                    form.find('#fl-field-uabb_row_linear_direction').show();
                }
                if (linear_advance_option == 'yes') {
                    form.find('#fl-field-uabb_row_linear_gradient_primary_loc').show();
                    form.find('#fl-field-uabb_row_linear_gradient_secondary_loc').show();
                }
            }
        }
    });
})(jQuery);
(function ($) {
    var form = $('.fl-builder-settings'),
        gradient_type = form.find('input[name=uabb_col_gradient_type]');
    $(document).on('change', ' input[name=uabb_col_radial_advance_options], input[name=uabb_col_linear_advance_options], input[name=uabb_col_gradient_type], select[name=bg_type]', function () {
        var form = $('.fl-builder-settings'),
            background_type = form.find('select[name=bg_type]').val(),
            linear_direction = form.find('select[name=uabb_col_uabb_direction]').val(),
            linear_advance_option = form.find('input[name=uabb_col_linear_advance_options]:checked').val(),
            radial_advance_option = form.find('input[name=uabb_col_radial_advance_options]:checked').val(),
            gradient_type = form.find('input[name=uabb_col_gradient_type]:checked').val();
        if (background_type == 'uabb_gradient') {
            if (gradient_type == 'radial') {
                setTimeout(function () {
                    form.find('#fl-field-uabb_col_linear_direction').hide();
                    form.find('#fl-field-uabb_col_linear_gradient_primary_loc').hide();
                    form.find('#fl-field-uabb_col_linear_gradient_secondary_loc').hide();
                }, 1);
                if (radial_advance_option == 'yes') {
                    form.find('#fl-field-uabb_col_radial_gradient_primary_loc').show();
                    form.find('#fl-field-uabb_col_radial_gradient_secondary_loc').show();
                }
            }
            if (gradient_type == 'linear') {
                setTimeout(function () {
                    form.find('#fl-field-uabb_col_radial_gradient_primary_loc').hide();
                    form.find('#fl-field-uabb_col_radial_gradient_secondary_loc').hide();
                }, 1);
                if (linear_direction == 'custom') {
                    form.find('#fl-field-uabb_col_linear_direction').show();
                }
                if (linear_advance_option == 'yes') {
                    form.find('#fl-field-uabb_col_linear_gradient_primary_loc').show();
                    form.find('#fl-field-uabb_col_linear_gradient_secondary_loc').show();
                }
            }
        }
    });
})(jQuery);