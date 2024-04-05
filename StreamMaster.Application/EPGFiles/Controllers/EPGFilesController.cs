﻿using Microsoft.AspNetCore.Mvc;

using StreamMaster.Application.EPGFiles.Commands;

namespace StreamMaster.Application.EPGFiles;

public partial class EPGFilesController
{
    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<DefaultAPIResponse>> CreateEPGFileFromForm([FromForm] CreateEPGFileRequest request)
    {
        DefaultAPIResponse entity = await Sender.Send(request).ConfigureAwait(false);
        return entity == null ? DefaultAPIResponse.Error : DefaultAPIResponse.Success;
    }
}