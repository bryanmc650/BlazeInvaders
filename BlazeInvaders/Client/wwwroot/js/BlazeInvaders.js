window.BlazeInvadersSound = {
    loadSounds: function (sounds) {
        soundAudios = sounds;
        try {
            for (var key in sounds) {
                var value = sounds[key];
                soundAudios[key] = new window.Audio(value);
            }

            return true;
        } catch (e) {
            console.error(e);
            return false;
        }
        return true;
    },

    play: function (id) {
        try {
            const audio = soundAudios[id];
            audio.currentTime = 0;
            audio.play();

            return true;
        } catch (e) {
            return false;
        }
    },
    pause: function (id) {
        try {
            const audio = soundAudios[id];
            audio.currentTime = 0;
            audio.pause();

            return true;
        } catch (e) {
            return false;
        }
    }
};

document.onkeydown = function (evt) {
    evt = evt || window.event;
    window.DotNet.invokeMethodAsync('BlazeInvaders.Client', 'KeyDownFromJS', evt.keyCode);
    if (evt.keyCode !== 116 && evt.keyCode !== 123)
        evt.preventDefault();
}
document.onkeyup = function (evt) {
    evt = evt || window.event;
    window.DotNet.invokeMethodAsync('BlazeInvaders.Client', 'KeyUpFromJS', evt.keyCode);

    //Prevent all but F5 and F12
    if (evt.keyCode !== 116 && evt.keyCode !== 123)
        evt.preventDefault();
};
function addResizeGameWindowEvent(id, dotNetGameWindowObjRef) {
    var element = document.getElementById(id);
    window.onresize = function () { dotNetGameWindowObjRef.invokeMethodAsync('OnGameWindowResize', element.clientHeight, element.clientWidth); };
    return true;
};