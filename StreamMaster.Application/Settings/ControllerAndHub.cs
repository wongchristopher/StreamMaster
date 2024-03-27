using Microsoft.AspNetCore.Mvc;
using StreamMaster.Application.Settings.Commands;

namespace StreamMaster.Application.Settings
{
    public partial class SettingsController(ISender Sender) : ApiControllerBase, ISettingsController
    {        

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<SettingDto>> GetSettings()
        {
            SettingDto ret = await Sender.Send(new GetSettings()).ConfigureAwait(false);
            return ret == null ? NotFound(ret) : Ok(ret);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<SDSystemStatus>> GetSystemStatus()
        {
            SDSystemStatus ret = await Sender.Send(new GetSystemStatus()).ConfigureAwait(false);
            return ret == null ? NotFound(ret) : Ok(ret);
        }

    }
}

namespace StreamMaster.Application.Hubs
{
    public partial class StreamMasterHub : ISettingsHub
    {
        public async Task<SettingDto> GetSettings()
        {
            SettingDto ret = await Sender.Send(new GetSettings()).ConfigureAwait(false);
            return ret;
        }

        public async Task<SDSystemStatus> GetSystemStatus()
        {
            SDSystemStatus ret = await Sender.Send(new GetSystemStatus()).ConfigureAwait(false);
            return ret;
        }

    }
}
