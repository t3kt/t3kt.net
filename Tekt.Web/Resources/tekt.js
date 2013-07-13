var tekt = {
  waitStart: function ()
  {
    $('#wait-overlay').remove();
    $('body').append($('<div id="wait-overlay"><div id="wait-image">&nbsp;</div></div>'));
  },
  waitEnd: function ()
  {
    $('#wait-overlay').remove();
  }
};

$(function ()
{
  return;
  $(document).pjax('a.nav-link', '#main');
  $(document).on('pjax:send', function ()
  {
    tekt.waitStart();
  });
  $(document).on('pjax:complete', function ()
  {
    tekt.waitEnd();
  });
});
