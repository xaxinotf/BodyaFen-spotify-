using Microsoft.AspNetCore.SignalR;

namespace BodyaFen_spotify_.Dopomoga;

public class SongsHub : Hub
{
    public async Task NotifyNewSong()
    {
        await Clients.All.SendAsync("RefreshSongs");
    }
}