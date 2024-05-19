﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StreamMaster.Infrastructure.EF.PGSQL.Logging;

#nullable disable

namespace StreamMaster.Infrastructure.EF.PGSQL.Migrations.Logging
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20240211120810_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "citext");
            NpgsqlModelBuilderExtensions.UseIdentityAlwaysColumns(modelBuilder);

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

                    b.ToTable("ChannelGroup");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("LogLevel")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<bool>("AutoSetChannelNumbers")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("citext");

                    b.HasKey("Id");

                    b.ToTable("StreamGroup");
                });

            modelBuilder.Entity("StreamMaster.Domain.Models.StreamGroupChannelGroup", b =>
                {
                    b.Property<int>("ChannelGroupId")
                        .HasColumnType("integer");

                    b.Property<int>("StreamGroupId")
                        .HasColumnType("integer");

                    b.HasKey("ChannelGroupId", "StreamGroupId");

                    b.HasIndex("StreamGroupId");

                    b.ToTable("StreamGroupChannelGroup");
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

                    b.ToTable("StreamGroupVideoStream");
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

                    b.Property<string>("TimeShift")
                        .IsRequired()
                        .HasColumnType("text");

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

                    b.ToTable("VideoStream");
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

                    b.ToTable("VideoStreamLink");
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
