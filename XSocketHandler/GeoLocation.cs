using System;
using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;

namespace XSocketHandler
{
    /// <summary>
    /// This is a realtime controller. All you need is to inherit XSocketController and this will be registered in the server.
    /// </summary>
    public class GeoLocation : XSocketController
    {
        public ViewModel ViewModel { get; set; }

        public GeoLocation()
        {
            this.ViewModel = new ViewModel();
            this.OnClientConnect += GeoLocation_OnClientConnect;
        }

        /// <summary>
        /// When a client connects we expect a city parameter to be attached. See default-3.htm (and readme.txt)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void GeoLocation_OnClientConnect(object sender, XSockets.Core.Common.Socket.Event.Arguments.OnClientConnectArgs e)
        {
            try
            {
                this.ViewModel.City = e.XNode.XSocket.ProtocolInstance.Parameters["City"];
            }
            catch (Exception ex)
            {
                //Note: To catch errors in the client listen for error like the example below.
                //ws.bind(XSockets.Events.onError, function (err) {
                //  console.log('Error', err);
                //});
                this.DispatchError(ex, "Whops... error when reading the City parameter. Probably because you ran default-1.htm or default-2.htm. The City parameter was added in default-3.htm");
            }
        }

        /// <summary>
        /// This is a "actionmethod"
        /// ___________________________
        /// It can be called from the javascript API like this.
        /// var json = {
        ///        City: $('#cities').val()
        ///     };
        /// ws.trigger('setcity', json);
        /// 
        /// The model binder will convert json into our expected object "ViewModel"
        /// 
        /// this.Send(...) will send a message to the client triggering the event.      
        /// </summary>
        /// <param name="model"></param>
        public void SetCity(ViewModel model)
        {
            try
            {
                this.ViewModel.City = model.City;
                this.Send(model, "SetCity");
            }
            catch (Exception ex)
            {
                //Note: To catch errors in the client listen for error like the example below.
                //ws.bind(XSockets.Events.onError, function (err) {
                //  console.log('Error', err);
                //});
                this.DispatchError(ex, "Exception in SetCity");
            }
        }

        /// <summary>
        /// Another "actionmethod" that you can trigger from the JsAPI like.
        /// var json = {
        ///        Message: $('#message').val(),
        ///        City: $('#cities').val()
        ///     };
        /// ws.trigger('sendtocity', json);
        /// 
        /// Note: this.SendTo filters where to sendthe message, we only want to send it to the city targeted.
        /// </summary>
        /// <param name="model"></param>
        public void SendToCity(ViewModel model)
        {
            try
            {
                this.SendTo(n => ((GeoLocation)n.XSocket).ViewModel.City == model.City, model, "SendToCity");
            }
            catch (Exception ex)
            {
                //Note: To catch errors in the client listen for error like the example below.
                //ws.bind(XSockets.Events.onError, function (err) {
                //  console.log('Error', err);
                //});
                this.DispatchError(ex, "Exception in SendToCity");
            }
        }
    }

    /// <summary>
    /// A simple viewmodel for our simple example :)
    /// </summary>
    public class ViewModel
    {
        public string City { get; set; }
        public string Message { get; set; }
    }
}
