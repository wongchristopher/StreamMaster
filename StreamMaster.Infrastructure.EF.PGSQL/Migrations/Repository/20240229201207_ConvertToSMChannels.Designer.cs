﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StreamMaster.Infrastructure.EF.PGSQL;

#nullable disable

namespace StreamMaster.Infrastructure.EF.PGSQL.Migrations.Repository
{
    [DbContext(typeof(PGSQLRepositoryContext))]
    [Migration("20240229201207_ConvertToSMChannels")]
    partial class ConvertToSMChannels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "citext");
            NpgsqlModelBuilderExtensions.UseIdentityAlwaysColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("FriendlyName")
                        .HasColumnType("text");

                    b.Property<string>("Xml")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys");
                });

            modelBuilder.Entity("StreamMaster.ChannelGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsHidden")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("RegexMatch")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("idx_Name");

                    b.HasIndex("Name", "IsHidden")
                        .HasDatabaseName("idx_Name_IsHidden");

                    b.ToTable("ChannelGroups");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.EPGFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<bool>("AutoUpdate")
                        .HasColumnType("boolean");

                    b.Property<int>("ChannelCount")
                        .HasColumnType("integer");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("DownloadErrors")
                        .HasColumnType("integer");

                    b.Property<int>("EPGNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("FileExists")
                        .HasColumnType("boolean");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("HoursToUpdate")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastDownloadAttempt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastDownloaded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MinimumMinutesBetweenDownloads")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("ProgrammeCount")
                        .HasColumnType("integer");

                    b.Property<int>("SMFileType")
                        .HasColumnType("integer");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("TimeShift")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.ToTable("EPGFiles");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.M3UFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<bool>("AutoUpdate")
                        .HasColumnType("boolean");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("DownloadErrors")
                        .HasColumnType("integer");

                    b.Property<bool>("FileExists")
                        .HasColumnType("boolean");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("HoursToUpdate")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastDownloadAttempt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastDownloaded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MaxStreamCount")
                        .HasColumnType("integer");

                    b.Property<int>("MinimumMinutesBetweenDownloads")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<bool>("OverwriteChannelNumbers")
                        .HasColumnType("boolean");

                    b.Property<int>("SMFileType")
                        .HasColumnType("integer");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("StartingChannelNumber")
                        .HasColumnType("integer");

                    b.Property<int>("StationCount")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("citext");

                    b.Property<List<string>>("VODTags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("M3UFiles");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.SMChannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("ChannelNumber")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("EPGId")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("boolean");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("StationId")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("StreamingProxyType")
                        .HasColumnType("integer");

                    b.Property<int>("TimeShift")
                        .HasColumnType("integer");

                    b.Property<int>("VideoStreamHandler")
                        .HasColumnType("integer");

                    b.Property<string>("VideoStreamId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("idx_SMChannelName");

                    b.ToTable("SMChannels");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.SMChannelStreamLink", b =>
                {
                    b.Property<int>("SMChannelId")
                        .HasColumnType("integer");

                    b.Property<string>("SMStreamId")
                        .HasColumnType("citext");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.HasKey("SMChannelId", "SMStreamId");

                    b.HasIndex("SMStreamId");

                    b.ToTable("SMChannelStreamLinks");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.SMStream", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("citext");

                    b.Property<string>("EPGID")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("FilePosition")
                        .HasColumnType("integer");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUserCreated")
                        .HasColumnType("boolean");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("M3UFileId")
                        .HasColumnType("integer");

                    b.Property<string>("M3UFileName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("ShortId")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("StationId")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("Tvg_chno")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("idx_SMStreamName");

                    b.ToTable("SMStreams");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<bool>("AutoSetChannelNumbers")
                        .HasColumnType("boolean");

                    b.Property<string>("FFMPEGProfileId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.ToTable("StreamGroups");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroupChannelGroup", b =>
                {
                    b.Property<int>("ChannelGroupId")
                        .HasColumnType("integer");

                    b.Property<int>("StreamGroupId")
                        .HasColumnType("integer");

                    b.HasKey("ChannelGroupId", "StreamGroupId");

                    b.HasIndex("StreamGroupId");

                    b.ToTable("StreamGroupChannelGroups");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroupSMChannel", b =>
                {
                    b.Property<int>("SMChannelId")
                        .HasColumnType("integer");

                    b.Property<int>("StreamGroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("boolean");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.HasKey("SMChannelId", "StreamGroupId");

                    b.ToTable("StreamGroupSMChannels");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroupVideoStream", b =>
                {
                    b.Property<string>("ChildVideoStreamId")
                        .HasColumnType("citext");

                    b.Property<int>("StreamGroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("boolean");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.HasKey("ChildVideoStreamId", "StreamGroupId");

                    b.HasIndex("StreamGroupId");

                    b.ToTable("StreamGroupVideoStreams");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.SystemKeyValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.ToTable("SystemKeyValues");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.VideoStream", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("citext");

                    b.Property<int>("FilePosition")
                        .HasColumnType("integer");

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUserCreated")
                        .HasColumnType("boolean");

                    b.Property<int>("M3UFileId")
                        .HasColumnType("integer");

                    b.Property<string>("M3UFileName")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("ShortId")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("StationId")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("StreamProxyType")
                        .HasColumnType("integer");

                    b.Property<int>("StreamingProxyType")
                        .HasColumnType("integer");

                    b.Property<int>("TimeShift")
                        .HasColumnType("integer");

                    b.Property<string>("Tvg_ID")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("Tvg_chno")
                        .HasColumnType("integer");

                    b.Property<string>("Tvg_group")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Tvg_logo")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Tvg_name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("User_Tvg_ID")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("User_Tvg_chno")
                        .HasColumnType("integer");

                    b.Property<string>("User_Tvg_group")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("User_Tvg_logo")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("User_Tvg_name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<string>("User_Url")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<int>("VideoStreamHandler")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ShortId")
                        .HasDatabaseName("IX_VideoStream_ShortId");

                    b.HasIndex("User_Tvg_chno")
                        .HasDatabaseName("IX_VideoStream_User_Tvg_chno");

                    b.HasIndex("User_Tvg_group")
                        .HasDatabaseName("idx_User_Tvg_group");

                    b.HasIndex("User_Tvg_name")
                        .HasDatabaseName("IX_VideoStream_User_Tvg_name");

                    b.HasIndex("User_Tvg_group", "IsHidden")
                        .HasDatabaseName("idx_User_Tvg_group_IsHidden");

                    b.ToTable("VideoStreams");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.VideoStreamLink", b =>
                {
                    b.Property<string>("ParentVideoStreamId")
                        .HasColumnType("citext");

                    b.Property<string>("ChildVideoStreamId")
                        .HasColumnType("citext");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.HasKey("ParentVideoStreamId", "ChildVideoStreamId");

                    b.HasIndex("ChildVideoStreamId");

                    b.ToTable("VideoStreamLinks");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.SMChannelStreamLink", b =>
                {
                    b.HasOne("StreamMaster.Domain.Models.SMChannel", "SMChannel")
                        .WithMany("SMStreams")
                        .HasForeignKey("SMChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamMaster.Domain.Models.SMStream", "SMStream")
                        .WithMany()
                        .HasForeignKey("SMStreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SMChannel");

                    b.Navigation("SMStream");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroupChannelGroup", b =>
                {
                    b.HasOne("StreamMaster.ChannelGroup", "ChannelGroup")
                        .WithMany()
                        .HasForeignKey("ChannelGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamMaster.Domain.Models.StreamGroup", "StreamGroup")
                        .WithMany("ChannelGroups")
                        .HasForeignKey("StreamGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChannelGroup");

                    b.Navigation("StreamGroup");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroupSMChannel", b =>
                {
                    b.HasOne("StreamMaster.Domain.Models.SMChannel", "SMChannel")
                        .WithMany("StreamGroups")
                        .HasForeignKey("SMChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SMChannel");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroupVideoStream", b =>
                {
                    b.HasOne("StreamMaster.Domain.Models.VideoStream", "ChildVideoStream")
                        .WithMany("StreamGroups")
                        .HasForeignKey("ChildVideoStreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamMaster.Domain.Models.StreamGroup", null)
                        .WithMany("ChildVideoStreams")
                        .HasForeignKey("StreamGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChildVideoStream");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.VideoStreamLink", b =>
                {
                    b.HasOne("StreamMaster.Domain.Models.VideoStream", "ChildVideoStream")
                        .WithMany()
                        .HasForeignKey("ChildVideoStreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreamMaster.Domain.Models.VideoStream", "ParentVideoStream")
                        .WithMany("ChildVideoStreams")
                        .HasForeignKey("ParentVideoStreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChildVideoStream");

                    b.Navigation("ParentVideoStream");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.SMChannel", b =>
                {
                    b.Navigation("SMStreams");

                    b.Navigation("StreamGroups");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroup", b =>
                {
                    b.Navigation("ChannelGroups");

                    b.Navigation("ChildVideoStreams");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.VideoStream", b =>
                {
                    b.Navigation("ChildVideoStreams");

                    b.Navigation("StreamGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
