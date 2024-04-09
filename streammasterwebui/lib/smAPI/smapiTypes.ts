export interface QueryStringParameters
{
	JSONArgumentString?: string;
	JSONFiltersString?: string;
	OrderBy: string;
	PageNumber: number;
	PageSize: number;
}
export interface SMChannelRankRequest
{
	Rank: number;
	SMChannelId: number;
	SMStreamId: string;
}
export interface FieldData
{
	Entity: string;
	Field?: string;
	Id: string;
	Value?: any;
}
export interface SMMessage
{
	Detail?: string;
	Severity: string;
	Summary: string;
}
export interface StreamGroupSMChannel
{
	IsReadOnly: boolean;
	Rank: number;
	SMChannel: any;
	SMChannelId: number;
	StreamGroupId: number;
}
export interface ChannelGroupDto
{
	ActiveCount: number;
	ChannelGroupId: number;
	HiddenCount: number;
	Id: number;
	IsHidden: boolean;
	IsReadOnly: boolean;
	Name: string;
	TotalCount: number;
}
export interface EPGColorDto
{
	Color: string;
	EPGNumber: number;
	Id: number;
	StationId: string;
}
export interface EPGFileDto
{
	AutoUpdate: boolean;
	ChannelCount: number;
	Color: string;
	Description: string;
	DownloadErrors: number;
	EPGNumber: number;
	EPGStartDate: any;
	EPGStopDate: any;
	HoursToUpdate: number;
	Id: number;
	LastDownloadAttempt: any;
	LastDownloaded: any;
	Name: string;
	NeedsUpdate: boolean;
	ProgrammeCount: number;
	Source: string;
	TimeShift: number;
	Url: string;
}
export interface EPGFilePreviewDto
{
	ChannelLogo: string;
	ChannelName: string;
	ChannelNumber: string;
	Id: number;
}
export interface IconFileDto
{
	Extension: string;
	FileId: number;
	Id: string;
	Name: string;
	SMFileType: number;
	Source: string;
}
export interface M3UFileDto
{
	AutoUpdate: boolean;
	Description: string;
	DownloadErrors: number;
	HoursToUpdate: number;
	Id: number;
	LastDownloadAttempt: any;
	LastDownloaded: any;
	MaxStreamCount: number;
	Name: string;
	NeedsUpdate: boolean;
	OverwriteChannelNumbers: boolean;
	Source: string;
	StartingChannelNumber: number;
	StationCount: number;
	Url: string;
	VODTags: string[];
}
export interface SDSystemStatus
{
	IsSystemReady: boolean;
}
export interface SettingDto
{
	AdminPassword: string;
	AdminUserName: string;
	ApiKey: string;
	AuthenticationMethod: number;
	BackupEnabled: boolean;
	BackupInterval: number;
	BackupVersionsToKeep: number;
	CacheIcons: boolean;
	CleanURLs: boolean;
	ClientUserAgent: string;
	DefaultIcon: string;
	DeviceID: string;
	DummyRegex: string;
	EnablePrometheus: boolean;
	EnableSSL: boolean;
	ExpectedServiceCount: number;
	FFMPEGDefaultOptions: string;
	FFMPegExecutable: string;
	FFMpegOptions: string;
	FFProbeExecutable: string;
	GlobalStreamLimit: number;
	HLS: HLSSettings;
	IsDebug: boolean;
	LogPerformance: string[];
	M3UFieldGroupTitle: boolean;
	M3UIgnoreEmptyEPGID: boolean;
	M3UStationId: boolean;
	M3UUseChnoForId: boolean;
	M3UUseCUIDForChannelID: boolean;
	MaxConcurrentDownloads: number;
	MaxConnectRetry: number;
	MaxConnectRetryTimeMS: number;
	MaxLogFiles: number;
	MaxLogFileSizeMB: number;
	MaxStreamReStart: number;
	NameRegex: string[];
	PrettyEPG: boolean;
	Release: string;
	SDSettings: SDSettings;
	ShowClientHostNames: boolean;
	SSLCertPassword: string;
	SSLCertPath: string;
	StreamingClientUserAgent: string;
	StreamingProxyType: number;
	UiFolder: string;
	UrlBase: string;
	Version: string;
	VideoStreamAlwaysUseEPGLogo: boolean;
}
export interface SMChannelDto
{
	ChannelNumber: number;
	EPGId: string;
	Group: string;
	GroupTitle: string;
	Id: number;
	IsHidden: boolean;
	Logo: string;
	Name: string;
	RealUrl: string;
	SMStreams: SMStreamDto[];
	StationId: string;
	StreamGroups: StreamGroupSMChannel[];
	StreamingProxyType: number;
	TimeShift: number;
	VideoStreamHandler: number;
	VideoStreamId: string;
}
export interface SMStreamDto
{
	ChannelNumber: number;
	EPGID: string;
	FilePosition: number;
	Group: string;
	Id: string;
	IsHidden: boolean;
	IsUserCreated: boolean;
	Logo: string;
	M3UFileId: number;
	M3UFileName: string;
	Name: string;
	Rank: number;
	RealUrl: string;
	ShortId: string;
	StationId: string;
	Url: string;
}
export interface StreamGroupDto
{
	AutoSetChannelNumbers: boolean;
	FFMPEGProfileId: string;
	HDHRLink: string;
	Id: number;
	IsLoading: boolean;
	IsReadOnly: boolean;
	M3ULink: string;
	Name: string;
	ShortEPGLink: string;
	ShortM3ULink: string;
	StreamCount: number;
	XMLLink: string;
}
export interface HLSSettings
{
	HLSFFMPEGOptions: string;
	HLSM3U8CreationTimeOutInSeconds: number;
	HLSM3U8Enable: boolean;
	HLSM3U8ReadTimeOutInSeconds: number;
	HLSReconnectDurationInSeconds: number;
	HLSSegmentCount: number;
	HLSSegmentDurationInSeconds: number;
	HLSTSReadTimeOutInSeconds: number;
}
export interface SDSettings
{
	AlternateLogoStyle: string;
	AlternateSEFormat: boolean;
	AppendEpisodeDesc: boolean;
	ArtworkSize: string;
	ExcludeCastAndCrew: boolean;
	PreferredLogoStyle: string;
	PrefixEpisodeDescription: boolean;
	PrefixEpisodeTitle: boolean;
	SDCountry: string;
	SDEnabled: boolean;
	SDEPGDays: number;
	SDPassword: string;
	SDPostalCode: string;
	SDStationIds: any[];
	SDUserName: string;
	SeasonEventImages: boolean;
	SeriesPosterArt: boolean;
	SeriesPosterAspect: string;
	SeriesWsArt: boolean;
	XmltvAddFillerData: boolean;
	XmltvExtendedInfoInTitleDescriptions: boolean;
	XmltvFillerProgramLength: number;
	XmltvIncludeChannelNumbers: boolean;
	XmltvSingleImage: boolean;
}
export interface APIResponse
{
	ErrorMessage?: string;
	IsError: boolean;
	Message?: string;
	NotFound: APIResponse;
}
export interface DataResponse<T>
{
	_totalItemCount?: number;
	Count: number;
	Data: T;
	ErrorMessage?: string;
	IsError: boolean;
	Message?: string;
	NotFound: DataResponse<T>;
	Ok: DataResponse<T>;
	TotalItemCount: number;
}
export interface NoClass
{
}
export interface PagedResponse<T>
{
	_totalItemCount?: number;
	Count: number;
	Data: T[];
	ErrorMessage?: string;
	First: number;
	IsError: boolean;
	Message?: string;
	NotFound: DataResponse<T[]>;
	Ok: DataResponse<T[]>;
	PageNumber: number;
	PageSize: number;
	TotalItemCount: number;
	TotalPageCount: number;
}
export interface GetPagedStreamGroupsRequest
{
	Parameters: QueryStringParameters;
}
export interface GetPagedSMStreamsRequest
{
	Parameters: QueryStringParameters;
}
export interface ToggleSMStreamVisibleByIdRequest
{
	Id: string;
}
export interface SendSMErrorRequest
{
	Detail: string;
	Summary: string;
}
export interface SendSMInfoRequest
{
	Detail: string;
	Summary: string;
}
export interface SendSMMessageRequest
{
	Message: SMMessage;
}
export interface SendSMWarnRequest
{
	Detail: string;
	Summary: string;
}
export interface SendSuccessRequest
{
	Detail: string;
	Summary: string;
}
export interface GetPagedSMChannelsRequest
{
	Parameters: QueryStringParameters;
}
export interface CreateSMChannelFromStreamRequest
{
	StreamId: string;
}
export interface DeleteSMChannelRequest
{
	SMChannelId: number;
}
export interface DeleteSMChannelsFromParametersRequest
{
	Parameters: QueryStringParameters;
}
export interface DeleteSMChannelsRequest
{
	SMChannelIds: number[];
}
export interface SetSMChannelLogoRequest
{
	Logo: string;
	SMChannelId: number;
}
export interface SetSMChannelNameRequest
{
	Name: string;
	SMChannelId: number;
}
export interface SetSMChannelNumberRequest
{
	ChannelNumber: number;
	SMChannelId: number;
}
export interface GetSMChannelStreamsRequest
{
	SMChannelId: number;
}
export interface AddSMStreamToSMChannelRequest
{
	SMChannelId: number;
	SMStreamId: string;
}
export interface RemoveSMStreamFromSMChannelRequest
{
	SMChannelId: number;
	SMStreamId: string;
}
export interface SetSMStreamRanksRequest
{
	Requests: SMChannelRankRequest[];
}
export interface GetIsSystemReadyRequest
{
}
export interface GetSettingsRequest
{
}
export interface GetSystemStatusRequest
{
}
export interface GetStationChannelNamesSimpleQuery
{
	Parameters: any;
}
export interface AddLineup
{
	lineup: string;
}
export interface StationRequest
{
	LineUp: string;
	StationId: string;
}
export interface AddStation
{
	Requests: StationRequest[];
}
export interface EPGSync
{
}
export interface RemoveLineup
{
	lineup: string;
}
export interface RemoveStation
{
	Requests: StationRequest[];
}
export interface GetPagedM3UFilesRequest
{
	Parameters: QueryStringParameters;
}
export interface CreateM3UFileFromFormRequest
{
	FormFile?: any;
	MaxStreamCount: number;
	Name: string;
	OverWriteChannels?: boolean;
	StartingChannelNumber?: number;
	VODTags?: string[];
}
export interface CreateM3UFileRequest
{
	MaxStreamCount: number;
	Name: string;
	OverWriteChannels?: boolean;
	StartingChannelNumber?: number;
	UrlSource?: string;
	VODTags?: string[];
}
export interface DeleteM3UFileRequest
{
	DeleteFile: boolean;
	Id: number;
}
export interface ProcessM3UFileRequest
{
	ForceRun: boolean;
	M3UFileId: number;
}
export interface RefreshM3UFileRequest
{
	ForceRun: boolean;
	Id: number;
}
export interface UpdateM3UFileRequest
{
	AutoUpdate?: boolean;
	HoursToUpdate?: number;
	Id: number;
	MaxStreamCount?: number;
	Name?: string;
	OverWriteChannels?: boolean;
	StartingChannelNumber?: number;
	Url?: string;
	VODTags?: string[];
}
export interface GetIconsRequest
{
}
export interface GetEPGColorsRequest
{
}
export interface GetEPGFilePreviewByIdRequest
{
	Id: number;
}
export interface GetEPGNextEPGNumberRequest
{
}
export interface GetPagedEPGFilesRequest
{
	Parameters: QueryStringParameters;
}
export interface CreateEPGFileRequest
{
	Color?: string;
	EPGNumber: number;
	FileName: string;
	FormFile?: any;
	Name: string;
	TimeShift?: number;
	UrlSource?: string;
}
export interface DeleteEPGFileRequest
{
	DeleteFile: boolean;
	Id: number;
}
export interface ProcessEPGFileRequest
{
	Id: number;
}
export interface RefreshEPGFileRequest
{
	Id: number;
}
export interface UpdateEPGFileRequest
{
	AutoUpdate?: boolean;
	Color?: string;
	EPGNumber?: number;
	HoursToUpdate?: number;
	Id: number;
	Name?: string;
	TimeShift?: number;
	Url?: string;
}
export interface GetPagedChannelGroupsRequest
{
	Parameters: QueryStringParameters;
}
export interface CreateChannelGroupRequest
{
	GroupName: string;
	IsReadOnly: boolean;
}
export interface DeleteAllChannelGroupsFromParametersRequest
{
	Parameters: QueryStringParameters;
}
export interface DeleteChannelGroupRequest
{
	ChannelGroupId: number;
}
export interface UpdateChannelGroupRequest
{
	ChannelGroupId: number;
	IsHidden?: boolean;
	NewGroupName?: string;
	ToggleVisibility?: boolean;
}
