﻿using AutoMapper;

namespace StreamMaster.Domain.Mappings;

public interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        if (typeof(T) == typeof(VideoStream))
        {
            profile.CreateMap<VideoStream, VideoStreamDto>(MemberList.None)
            .ForMember(dest => dest.ChildVideoStreams, opt => opt.MapFrom(src => src.ChildVideoStreams.Select(cr => cr.ChildVideoStream)));
            return;
        }


        if (typeof(T) == typeof(SMChannel))
        {
            profile.CreateMap<SMChannel, SMChannelDto>(MemberList.None)
              .ForMember(dest => dest.SMStreams, opt => opt.MapFrom(src => src.SMStreams
              .Where(cr => cr.SMStream != null)
              .Select(cr => cr.SMStream)
              ));

            return;

        }


        //if (typeof(T) == typeof(StreamGroup))
        //{
        //    profile.CreateMap<StreamGroup, StreamGroupDto>(MemberList.None)
        //    .ForMember(dest => dest.ChannelGroups, opt => opt.MapFrom(src => src.ChannelGroups.Select(cr => cr.ChannelGroup)))
        //    .ForMember(dest => dest.ChildVideoStreams, opt => opt.MapFrom(src => src.ChildVideoStreams.Select(cr => cr.ChildVideoStream)));            
        //    return;
        //}

        _ = profile.CreateMap(typeof(T), GetType(), MemberList.None);
    }
}
