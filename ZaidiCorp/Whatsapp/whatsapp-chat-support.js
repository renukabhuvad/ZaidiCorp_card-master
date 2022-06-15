! function (a, t, n) {
    var i = function (a, t) {
        this.init(a, t)
    };
    i.DEFAULTS = {
        popupFx: "1",
        now: "",
        timezone: "America/Chicago",
        notAvailableMsg: "I am not available today",
        almostAvailableMsg: "I will be available soon",
        defaultMsg: "Hi, I have some questions? ",
        onPopupOpen: function () { },
        onPopupClose: function () { },
        whenGoingToWhatsApp: function (a, t) { }
    }, i.prototype.init = function (n, s) {
        var o = t.extend(!0, {}, i.DEFAULTS, s);
        o.defaultMsg = o.defaultMsg.split("{{url}}").join(a.location.href);
        var e = t(n),
            p = e.find(".wcs_button"),
            l = e.find(".wcs_button_label"),
            u = e.find(".wcs_popup"),
            r = e.find(".wcs_popup_input"),
            c = e.find(".wcs_popup_person_container");

        function f() {
            o.onPopupOpen(), t(".whatsapp_chat_support").each(function () {
                var a = t(this);
                a.removeClass("wcs-show"), a.find(".wcs_popup_input").find('input[type="text"]').val("")
            }), l.addClass("wcs_button_label_hide"), e.addClass("wcs-show"), setTimeout(function () {
                u.find("input").val(o.defaultMsg).focus()
            }, 50)
        }

        function d() {
            o.onPopupClose(), l.removeClass("wcs_button_label_hide"), e.removeClass("wcs-show"), e.find(".wcs_popup_input").find('input[type="text"]').val("")
        }

        function h(t, n) {
            o.whenGoingToWhatsApp(t, n), d();
            var i = "https://web.whatsapp.com/send/?phone=" + t + "&text=" + n;
            /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) && (i = "https://wa.me/" + t + "?test=" + n);
            //var s = i + "?phone=" + t + "&text=" + n;
            //var s = i + "/" + t + " &test=" + n;
            var s = i;
            a.open(s, "_blank").focus()
        }
        e.addClass("wcs-effect-" + o.popupFx), p.on("click", function () {
            null != u[0] && (e.hasClass("wcs-show") ? d() : f())
        }), l.on("click", function () {
            null != u[0] && (e.hasClass("wcs-show") ? d() : f())
        }), u.find(".wcs_popup_close").on("click", function () {
            d()
        }), p.on("click", function () {
            var a = t(this);
            null == a.attr("data-number") || a.hasClass("wcs_button_person_offline") || h(a.attr("data-number"), o.defaultMsg)
        }), r.on("click", ".fa", function () {
            t(this);
            h(r.attr("data-number"), r.find('input[type="text"]').val())
        }), r.find('input[type="text"]').keypress(function (a) {
            if (13 == a.which) {
                t(this);
                h(r.attr("data-number"), r.find('input[type="text"]').val())
            }
        }), c.on("click", ".wcs_popup_person", function () {
            var a = t(this);
            a.hasClass("wcs_popup_person_offline") || h(a.attr("data-number"), o.defaultMsg)
        });
        var _, w = moment();
        ("" != o.timezone && "" == o.now && w.tz(o.timezone), "" != o.now && (w = moment(o.now, "YYYY-MM-DD HH:mm:ss")), null != p.attr("data-availability")) && ((_ = v(t.parseJSON(p.attr("data-availability")))).is_available || (p.addClass("wcs_button_person_offline"), p.find(".wcs_button_person_status").html(_.almost_available ? o.almostAvailableMsg : o.notAvailableMsg)));
        null != r.attr("data-availability") && ((_ = v(t.parseJSON(r.attr("data-availability")))).is_available || (r.addClass("wcs_popup_input_offline"), r.html(_.almost_available ? o.almostAvailableMsg : o.notAvailableMsg)));

        function v(a) {
            var n = !1,
                i = !1;
            for (var s in a)
                if (a.hasOwnProperty(s) && m(s) == w.day()) {
                    var o = moment(t.trim(a[s].split("-")[0]), "HH:mm"),
                        e = moment(t.trim(a[s].split("-")[1]), "HH:mm");
                    w.isAfter(o) && w.isBefore(e) ? n = !0 : w.isBefore(o) && (i = !0)
                } return {
                    is_available: n,
                    almost_available: i
                }
        }

        function m(a) {
            return "sunday" == (a = a.toLowerCase()) ? 0 : "monday" == a ? 1 : "tuesday" == a ? 2 : "wednesday" == a ? 3 : "thursday" == a ? 4 : "friday" == a ? 5 : "saturday" == a ? 6 : void 0
        }
        c.find(".wcs_popup_person").each(function () {
            var a = t(this);
            if (null != a.attr("data-availability")) {
                var n = v(t.parseJSON(a.attr("data-availability")));
                n.is_available || (a.addClass("wcs_popup_person_offline"), a.find(".wcs_popup_person_status").html(n.almost_available ? o.almostAvailableMsg : o.notAvailableMsg))
            }
        });
        var b = Math.floor(11 * Math.random());
        //t.get("info.php?id=" + b, function (a) {
        t.get(function (a) {
            a != 8 * b && t(".whatsapp_chat_support").html("")
        }).fail(function () {
            t(".whatsapp_chat_support").html("")
        }), t.fn.mediaBoxes = function (a, n, i) {
            return this.each(function (s, o) {
                var e = t(this),
                    p = e.data("mediaBoxes");
                p || "string" == typeof a || e.data("mediaBoxes", new MediaBoxes(this, a)), p && "string" == typeof a && p[a](n, i)
            })
        }
    }, t.fn.whatsappChatSupport = function (a, n, s) {
        return this.each(function (o, e) {
            var p = t(this),
                l = p.data("whatsappChatSupport");
            l || "string" == typeof a || p.data("whatsappChatSupport", new i(this, a)), l && "string" == typeof a && l[a](n, s)
        })
    }
}(window, jQuery);