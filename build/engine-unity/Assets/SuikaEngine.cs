/* -*- coding: utf-8; tab-width: 4; c-basic-offset: 4; indent-tabs-mode: t; -*- */

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SuikaEngine : MonoBehaviour
{
	// Init delegate types
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	 unsafe delegate void delegate_init_hal_func_table(IntPtr log_info,
													 IntPtr log_warn,
													 IntPtr log_error,
													 IntPtr make_sav_dir,
													 IntPtr make_valid_path,
													 IntPtr notify_image_update,
													 IntPtr notify_image_free,
													 IntPtr render_image_normal,
													 IntPtr render_image_add,
													 IntPtr render_image_dim,
													 IntPtr render_image_rule,
													 IntPtr render_image_melt,
													 IntPtr render_image_3d_normal,
													 IntPtr render_image_3d_add,
													 IntPtr reset_lap_timer,
													 IntPtr get_lap_timer_millisec,
													 IntPtr play_sound,
													 IntPtr stop_sound,
													 IntPtr set_sound_volume,
													 IntPtr is_sound_finished,
													 IntPtr play_video,
													 IntPtr stop_video,
													 IntPtr is_video_playing,
													 IntPtr update_window_title,
													 IntPtr is_full_screen_supported,
													 IntPtr is_full_screen_mode,
													 IntPtr enter_full_screen_mode,
													 IntPtr leave_full_screen_mode,
													 IntPtr get_system_locale,
													 IntPtr speak_text,
													 IntPtr set_continuous_swipe_enabled,
													 IntPtr free_shared,
													 IntPtr get_file_contents,
													 IntPtr open_save_file,
													 IntPtr write_save_file,
													 IntPtr close_save_file);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate int delegate_init_conf();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_init_locale_code();

	// Event delegate types
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate int delegate_on_event_init();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_on_event_cleanup();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate int delegate_on_event_frame();

	// HAL delegate types
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_log_info([In] IntPtr s);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_log_warn([In] IntPtr s);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_log_error([In] IntPtr s);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_make_sav_dir();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_make_valid_path([In] IntPtr dir, [In] IntPtr fname, [Out] IntPtr dst, int len);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_notify_image_update([Out] IntPtr img);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_notify_image_free([Out] IntPtr img);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_render_image_normal(int dst_left, int dst_top, int dst_width, int dst_height, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_render_image_add(int dst_left, int dst_top, int dst_width, int dst_height, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_render_image_dim(int dst_left, int dst_top, int dst_width, int dst_height, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_render_image_rule([Out] IntPtr src_img, [Out] IntPtr rule_img, int threshold);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_render_image_melt([Out] IntPtr src_img, [Out] IntPtr rule_img, int progress);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_render_image_3d_normal(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_render_image_3d_add(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_reset_lap_timer([Out] IntPtr origin);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate Int64 delegate_get_lap_timer_millisec([Out] IntPtr origin);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_play_sound(int stream, [Out] IntPtr wave);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_stop_sound(int stream);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_set_sound_volume(int stream, float vol);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate int delegate_is_sound_finished(int stream);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate int delegate_play_video([Out] IntPtr fname, int is_skippable);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_stop_video();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate int delegate_is_video_playing();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_update_window_title();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate int delegate_is_full_screen_supported();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate int delegate_is_full_screen_mode();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_enter_full_screen_mode();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_leave_full_screen_mode();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate IntPtr delegate_get_system_locale();
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_speak_text([In] IntPtr text);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_set_continuous_swipe_enabled(int is_enabled);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_free_shared([Out] IntPtr p);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate IntPtr delegate_get_file_contents([In] IntPtr pFileName, [Out] IntPtr len);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_open_save_file([In] IntPtr pFileName);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_write_save_file(int b);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)] unsafe delegate void delegate_close_save_file();

	// Delegate objects
	static delegate_init_hal_func_table d_init_hal_func_table = new delegate_init_hal_func_table(init_hal_func_table);
	static delegate_init_conf d_init_conf = new delegate_init_conf(init_conf);
	static delegate_init_locale_code d_init_locale_code = new delegate_init_locale_code(init_locale_code);
	static delegate_on_event_init d_on_event_init = new delegate_on_event_init(on_event_init);
	static delegate_on_event_cleanup d_on_event_cleanup = new delegate_on_event_cleanup(on_event_cleanup);
	static delegate_on_event_frame d_on_event_frame = new delegate_on_event_frame(on_event_frame);
	static delegate_log_info d_log_info = new delegate_log_info(log_info);
	static delegate_log_warn d_log_warn = new delegate_log_warn(log_warn);
	static delegate_log_error d_log_error = new delegate_log_error(log_error);
	static delegate_make_sav_dir d_make_sav_dir = new delegate_make_sav_dir(make_sav_dir);
	static delegate_make_valid_path d_make_valid_path = new delegate_make_valid_path(make_valid_path);
	static delegate_notify_image_update d_notify_image_update = new delegate_notify_image_update(notify_image_update);
	static delegate_notify_image_free d_notify_image_free = new delegate_notify_image_free(notify_image_free);
	static delegate_render_image_normal d_render_image_normal = new delegate_render_image_normal(render_image_normal);
	static delegate_render_image_add d_render_image_add = new delegate_render_image_add(render_image_add);
	static delegate_render_image_dim d_render_image_dim = new delegate_render_image_dim(render_image_dim);
	static delegate_render_image_rule d_render_image_rule = new delegate_render_image_rule(render_image_rule);
	static delegate_render_image_melt d_render_image_melt = new delegate_render_image_melt(render_image_melt);
	static delegate_render_image_3d_normal d_render_image_3d_normal = new delegate_render_image_3d_normal(render_image_3d_normal);
	static delegate_render_image_3d_add d_render_image_3d_add = new delegate_render_image_3d_add(render_image_3d_add);
	static delegate_reset_lap_timer d_reset_lap_timer = new delegate_reset_lap_timer(reset_lap_timer);
	static delegate_get_lap_timer_millisec d_get_lap_timer_millisec = new delegate_get_lap_timer_millisec(get_lap_timer_millisec);
	static delegate_play_sound d_play_sound = new delegate_play_sound(play_sound);
	static delegate_stop_sound d_stop_sound = new delegate_stop_sound(stop_sound);
	static delegate_set_sound_volume d_set_sound_volume = new delegate_set_sound_volume(set_sound_volume);
	static delegate_is_sound_finished d_is_sound_finished = new delegate_is_sound_finished(is_sound_finished);
	static delegate_play_video d_play_video = new delegate_play_video(play_video);
	static delegate_stop_video d_stop_video = new delegate_stop_video(stop_video);
	static delegate_is_video_playing d_is_video_playing = new delegate_is_video_playing(is_video_playing);
	static delegate_update_window_title d_update_window_title = new delegate_update_window_title(update_window_title);
	static delegate_is_full_screen_supported d_is_full_screen_supported = new delegate_is_full_screen_supported(is_full_screen_supported);
	static delegate_is_full_screen_mode d_is_full_screen_mode = new delegate_is_full_screen_mode(is_full_screen_mode);
	static delegate_enter_full_screen_mode d_enter_full_screen_mode = new delegate_enter_full_screen_mode(enter_full_screen_mode);
	static delegate_leave_full_screen_mode d_leave_full_screen_mode = new delegate_leave_full_screen_mode(leave_full_screen_mode);
	static delegate_get_system_locale d_get_system_locale = new delegate_get_system_locale(get_system_locale);
	static delegate_speak_text d_speak_text = new delegate_speak_text(speak_text);
	static delegate_set_continuous_swipe_enabled d_set_continuous_swipe_enabled = new delegate_set_continuous_swipe_enabled(set_continuous_swipe_enabled);
	static delegate_free_shared d_free_shared = new delegate_free_shared(free_shared);
	static delegate_get_file_contents d_get_file_contents = new delegate_get_file_contents(get_file_contents);
	static delegate_open_save_file d_open_save_file = new delegate_open_save_file(open_save_file);
	static delegate_write_save_file d_write_save_file = new delegate_write_save_file(write_save_file);
	static delegate_close_save_file d_close_save_file = new delegate_close_save_file(close_save_file);

	static IntPtr p_log_info;
	static IntPtr p_log_warn;
	static IntPtr p_log_error;
	static IntPtr p_make_sav_dir;
	static IntPtr p_make_valid_path;
	static IntPtr p_notify_image_update;
	static IntPtr p_notify_image_free;
	static IntPtr p_render_image_normal;
	static IntPtr p_render_image_add;
	static IntPtr p_render_image_dim;
	static IntPtr p_render_image_rule;
	static IntPtr p_render_image_melt;
	static IntPtr p_render_image_3d_normal;
	static IntPtr p_render_image_3d_add;
	static IntPtr p_reset_lap_timer;
	static IntPtr p_get_lap_timer_millisec;
	static IntPtr p_play_sound;
	static IntPtr p_stop_sound;
	static IntPtr p_set_sound_volume;
	static IntPtr p_is_sound_finished;
	static IntPtr p_play_video;
	static IntPtr p_stop_video;
	static IntPtr p_is_video_playing;
	static IntPtr p_update_window_title;
	static IntPtr p_is_full_screen_supported;
	static IntPtr p_is_full_screen_mode;
	static IntPtr p_enter_full_screen_mode;
	static IntPtr p_leave_full_screen_mode;
	static IntPtr p_get_system_locale;
	static IntPtr p_speak_text;
	static IntPtr p_set_continuous_swipe_enabled;
	static IntPtr p_free_shared;
	static IntPtr p_get_file_contents;
	static IntPtr p_open_save_file;
	static IntPtr p_write_save_file;
	static IntPtr p_close_save_file;

    unsafe void Start()
    {
		GC.KeepAlive(this);

		// Lock delegate objects.
		GCHandle.Alloc(d_init_locale_code, GCHandleType.Pinned);
		GCHandle.Alloc(d_init_hal_func_table, GCHandleType.Pinned);
		GCHandle.Alloc(d_init_conf = init_conf, GCHandleType.Pinned);
		GCHandle.Alloc(d_init_locale_code, GCHandleType.Pinned);
		GCHandle.Alloc(d_on_event_init, GCHandleType.Pinned);
		GCHandle.Alloc(d_on_event_cleanup, GCHandleType.Pinned);
		GCHandle.Alloc(d_on_event_frame, GCHandleType.Pinned);
		GCHandle.Alloc(d_log_info, GCHandleType.Pinned);
		GCHandle.Alloc(d_log_warn, GCHandleType.Pinned);
		GCHandle.Alloc(d_log_error, GCHandleType.Pinned);
		GCHandle.Alloc(d_make_sav_dir, GCHandleType.Pinned);
		GCHandle.Alloc(d_make_valid_path, GCHandleType.Pinned);
		GCHandle.Alloc(d_notify_image_update, GCHandleType.Pinned);
		GCHandle.Alloc(d_notify_image_free, GCHandleType.Pinned);
		GCHandle.Alloc(d_render_image_normal, GCHandleType.Pinned);
		GCHandle.Alloc(d_render_image_add, GCHandleType.Pinned);
		GCHandle.Alloc(d_render_image_dim, GCHandleType.Pinned);
		GCHandle.Alloc(d_render_image_rule, GCHandleType.Pinned);
		GCHandle.Alloc(d_render_image_melt, GCHandleType.Pinned);
		GCHandle.Alloc(d_render_image_3d_normal, GCHandleType.Pinned);
		GCHandle.Alloc(d_render_image_3d_add, GCHandleType.Pinned);
		GCHandle.Alloc(d_reset_lap_timer, GCHandleType.Pinned);
		GCHandle.Alloc(d_get_lap_timer_millisec, GCHandleType.Pinned);
		GCHandle.Alloc(d_play_sound, GCHandleType.Pinned);
		GCHandle.Alloc(d_stop_sound, GCHandleType.Pinned);
		GCHandle.Alloc(d_set_sound_volume, GCHandleType.Pinned);
		GCHandle.Alloc(d_is_sound_finished, GCHandleType.Pinned);
		GCHandle.Alloc(d_play_video, GCHandleType.Pinned);
		GCHandle.Alloc(d_stop_video, GCHandleType.Pinned);
		GCHandle.Alloc(d_is_video_playing, GCHandleType.Pinned);
		GCHandle.Alloc(d_update_window_title, GCHandleType.Pinned);
		GCHandle.Alloc(d_is_full_screen_supported, GCHandleType.Pinned);
		GCHandle.Alloc(d_is_full_screen_mode, GCHandleType.Pinned);
		GCHandle.Alloc(d_enter_full_screen_mode, GCHandleType.Pinned);
		GCHandle.Alloc(d_leave_full_screen_mode, GCHandleType.Pinned);
		GCHandle.Alloc(d_get_system_locale, GCHandleType.Pinned);
		GCHandle.Alloc(d_speak_text, GCHandleType.Pinned);
		GCHandle.Alloc(d_set_continuous_swipe_enabled, GCHandleType.Pinned);
		GCHandle.Alloc(d_free_shared, GCHandleType.Pinned);
		GCHandle.Alloc(d_get_file_contents, GCHandleType.Pinned);
		GCHandle.Alloc(d_open_save_file, GCHandleType.Pinned);
		GCHandle.Alloc(d_write_save_file, GCHandleType.Pinned);
		GCHandle.Alloc(d_close_save_file, GCHandleType.Pinned);

		// Get function pointers.
		p_log_info = Marshal.GetFunctionPointerForDelegate(d_log_info);
		p_log_warn = Marshal.GetFunctionPointerForDelegate(d_log_warn);
		p_log_error = Marshal.GetFunctionPointerForDelegate(d_log_error);
		p_make_sav_dir = Marshal.GetFunctionPointerForDelegate(d_make_sav_dir);
		p_make_valid_path = Marshal.GetFunctionPointerForDelegate(d_make_valid_path);
		p_notify_image_update = Marshal.GetFunctionPointerForDelegate(d_notify_image_update);
		p_notify_image_free = Marshal.GetFunctionPointerForDelegate(d_notify_image_free);
		p_render_image_normal = Marshal.GetFunctionPointerForDelegate(d_render_image_normal);
		p_render_image_add = Marshal.GetFunctionPointerForDelegate(d_render_image_add);
		p_render_image_dim = Marshal.GetFunctionPointerForDelegate(d_render_image_dim);
		p_render_image_rule = Marshal.GetFunctionPointerForDelegate(d_render_image_rule);
		p_render_image_melt = Marshal.GetFunctionPointerForDelegate(d_render_image_melt);
		p_render_image_3d_normal = Marshal.GetFunctionPointerForDelegate(d_render_image_3d_normal);
		p_render_image_3d_add = Marshal.GetFunctionPointerForDelegate(d_render_image_3d_add);
		p_reset_lap_timer = Marshal.GetFunctionPointerForDelegate(d_reset_lap_timer);
		p_get_lap_timer_millisec = Marshal.GetFunctionPointerForDelegate(d_get_lap_timer_millisec);
		p_play_sound = Marshal.GetFunctionPointerForDelegate(d_play_sound);
		p_stop_sound = Marshal.GetFunctionPointerForDelegate(d_stop_sound);
		p_set_sound_volume = Marshal.GetFunctionPointerForDelegate(d_set_sound_volume);
		p_is_sound_finished = Marshal.GetFunctionPointerForDelegate(d_is_sound_finished);
		p_play_video = Marshal.GetFunctionPointerForDelegate(d_play_video);
		p_stop_video = Marshal.GetFunctionPointerForDelegate(d_stop_video);
		p_is_video_playing = Marshal.GetFunctionPointerForDelegate(d_is_video_playing);
		p_update_window_title = Marshal.GetFunctionPointerForDelegate(d_update_window_title);
		p_is_full_screen_supported = Marshal.GetFunctionPointerForDelegate(d_is_full_screen_supported);
		p_is_full_screen_mode = Marshal.GetFunctionPointerForDelegate(d_is_full_screen_mode);
		p_enter_full_screen_mode = Marshal.GetFunctionPointerForDelegate(d_enter_full_screen_mode);
		p_leave_full_screen_mode = Marshal.GetFunctionPointerForDelegate(d_leave_full_screen_mode);
		p_get_system_locale = Marshal.GetFunctionPointerForDelegate(d_get_system_locale);
		p_speak_text = Marshal.GetFunctionPointerForDelegate(d_speak_text);
		p_set_continuous_swipe_enabled = Marshal.GetFunctionPointerForDelegate(d_set_continuous_swipe_enabled);
		p_free_shared = Marshal.GetFunctionPointerForDelegate(d_free_shared);
		p_get_file_contents = Marshal.GetFunctionPointerForDelegate(d_get_file_contents);
		p_open_save_file = Marshal.GetFunctionPointerForDelegate(d_open_save_file);
		p_write_save_file = Marshal.GetFunctionPointerForDelegate(d_write_save_file);
		p_close_save_file = Marshal.GetFunctionPointerForDelegate(d_close_save_file);

		// Lock function pointers.
		GC.KeepAlive(p_log_info);
		GC.KeepAlive(p_log_warn);
		GC.KeepAlive(p_log_error);
		GC.KeepAlive(p_make_sav_dir);
		GC.KeepAlive(p_make_valid_path);
		GC.KeepAlive(p_notify_image_update);
		GC.KeepAlive(p_notify_image_free);
		GC.KeepAlive(p_render_image_normal);
		GC.KeepAlive(p_render_image_add);
		GC.KeepAlive(p_render_image_dim);
		GC.KeepAlive(p_render_image_rule);
		GC.KeepAlive(p_render_image_melt);
		GC.KeepAlive(p_render_image_3d_normal);
		GC.KeepAlive(p_render_image_3d_add);
		GC.KeepAlive(p_reset_lap_timer);
		GC.KeepAlive(p_get_lap_timer_millisec);
		GC.KeepAlive(p_play_sound);
		GC.KeepAlive(p_stop_sound);
		GC.KeepAlive(p_set_sound_volume);
		GC.KeepAlive(p_is_sound_finished);
		GC.KeepAlive(p_play_video);
		GC.KeepAlive(p_stop_video);
		GC.KeepAlive(p_is_video_playing);
		GC.KeepAlive(p_update_window_title);
		GC.KeepAlive(p_is_full_screen_supported);
		GC.KeepAlive(p_is_full_screen_mode);
		GC.KeepAlive(p_enter_full_screen_mode);
		GC.KeepAlive(p_leave_full_screen_mode);
		GC.KeepAlive(p_get_system_locale);
		GC.KeepAlive(p_speak_text);
		GC.KeepAlive(p_set_continuous_swipe_enabled);
		GC.KeepAlive(p_free_shared);
		GC.KeepAlive(p_get_file_contents);
		GC.KeepAlive(p_open_save_file);
		GC.KeepAlive(p_write_save_file);
		GC.KeepAlive(p_close_save_file);

		// Initialize the HAL function table.
		d_init_hal_func_table(
			p_log_info,
			p_log_warn,
			p_log_error,
			p_make_sav_dir,
			p_make_valid_path,
			p_notify_image_update,
			p_notify_image_free,
			p_render_image_normal,
			p_render_image_add,
			p_render_image_dim,
			p_render_image_rule,
			p_render_image_melt,
			p_render_image_3d_normal,
			p_render_image_3d_add,
			p_reset_lap_timer,
			p_get_lap_timer_millisec,
			p_play_sound,
			p_stop_sound,
			p_set_sound_volume,
			p_is_sound_finished,
			p_play_video,
			p_stop_video,
			p_is_video_playing,
			p_update_window_title,
			p_is_full_screen_supported,
			p_is_full_screen_mode,
			p_enter_full_screen_mode,
			p_leave_full_screen_mode,
			p_get_system_locale,
			p_speak_text,
			p_set_continuous_swipe_enabled,
			p_free_shared,
			p_get_file_contents,
			p_open_save_file,
			p_write_save_file,
			p_close_save_file);
		GC.KeepAlive(this);

		// Initialize the locale code.
		d_init_locale_code();
		GC.KeepAlive(this);

		// Initialize the config subsystem.
		d_init_conf();
		GC.KeepAlive(this);

		// Initialize the event subsystem.
		d_on_event_init();
		GC.KeepAlive(this);
	}

    unsafe void Update()
    {
    }

	//
	// Native
	//

    [DllImport("libpolaris.dylib")]
    static extern unsafe void init_hal_func_table(
		IntPtr log_info,
		IntPtr log_warn,
		IntPtr log_error,
		IntPtr make_sav_dir,
		IntPtr make_valid_path,
		IntPtr notify_image_update,
		IntPtr notify_image_free,
		IntPtr render_image_normal,
		IntPtr render_image_add,
		IntPtr render_image_dim,
		IntPtr render_image_rule,
		IntPtr render_image_melt,
		IntPtr render_image_3d_normal,
		IntPtr render_image_3d_add,
		IntPtr reset_lap_timer,
		IntPtr get_lap_timer_millisec,
		IntPtr play_sound,
		IntPtr stop_sound,
		IntPtr set_sound_volume,
		IntPtr is_sound_finished,
		IntPtr play_video,
		IntPtr stop_video,
		IntPtr is_video_playing,
		IntPtr update_window_title,
		IntPtr is_full_screen_supported,
		IntPtr is_full_screen_mode,
		IntPtr enter_full_screen_mode,
		IntPtr leave_full_screen_mode,
		IntPtr get_system_locale,
		IntPtr speak_text,
		IntPtr set_continuous_swipe_enabled,
		IntPtr free_shared,
		IntPtr get_file_contents,
		IntPtr open_save_file,
		IntPtr write_save_file,
		IntPtr close_save_file);

    [DllImport("libpolaris")]
    static extern unsafe void init_locale_code();

    [DllImport("libpolaris")]
	static extern unsafe int init_conf();

    [DllImport("libpolaris")]
	static extern unsafe int on_event_init();

    [DllImport("libpolaris")]
    static extern unsafe void on_event_cleanup();

    [DllImport("libpolaris")]
	static extern unsafe int on_event_frame();

    [DllImport("libpolaris")]
    static extern unsafe void on_event_key_press(int key);

    [DllImport("libpolaris")]
    static extern unsafe void on_event_key_release(int key);

    [DllImport("libpolaris")]
    static extern unsafe void on_event_mouse_press(int button, int x, int y);

    [DllImport("libpolaris")]
    static extern unsafe void on_event_mouse_release(int button, int x, int y);

    [DllImport("libpolaris")]
    static extern unsafe void on_event_mouse_move(int x, int y);

    [DllImport("libpolaris")]
    static extern unsafe void on_event_touch_cancel();

    [DllImport("libpolaris")]
    static extern unsafe void on_event_swipe_down();

    [DllImport("libpolaris")]
    static extern unsafe void on_event_swipe_up();

	//
	// HAL
	//

	struct Image {
		int width;
		int height;
		IntPtr pixels;
		IntPtr texture;
		bool need_upload;
	};

	static unsafe IntPtr locale = IntPtr.Zero;

	[AOT.MonoPInvokeCallback(typeof(delegate_log_info))]
	static unsafe void log_info(IntPtr s)
	{
		GCHandle gcHandle = GCHandle.Alloc(s, GCHandleType.Pinned);
		
		string str = IntPtrToString(s);
		Debug.Log(str);

		gcHandle.Free();
		GC.KeepAlive(s);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_log_warn))]
	static unsafe void log_warn(IntPtr s)
	{
		GCHandle gcHandle = GCHandle.Alloc(s, GCHandleType.Pinned);

		string str = IntPtrToString(s);
		Debug.Log(str);

		gcHandle.Free();
		GC.KeepAlive(s);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_log_error))]
	static unsafe void log_error(IntPtr s)
	{
		GCHandle gcHandle = GCHandle.Alloc(s, GCHandleType.Pinned);

		string str = IntPtrToString(s);
		Debug.Log(str);

		gcHandle.Free();
		GC.KeepAlive(s);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_make_sav_dir))]
	static unsafe void make_sav_dir()
	{
		// TODO
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_make_valid_path))]
	static unsafe void make_valid_path([In] IntPtr dir, [In] IntPtr fname, [Out] IntPtr dst, int len)
	{
		GCHandle gcHandleDir = GCHandle.Alloc(dir, GCHandleType.Pinned);
		GCHandle gcHandleFile = GCHandle.Alloc(fname, GCHandleType.Pinned);
		GCHandle gcHandleDst = GCHandle.Alloc(dst, GCHandleType.Pinned);
		GC.KeepAlive(dst);
		IntPtr dummy = gcHandleDst.AddrOfPinnedObject();
		GC.KeepAlive(dummy);

		string Path = "";
		if (dir != IntPtr.Zero)
			Path = Path + IntPtrToString(dir) + "/";
		if (fname != IntPtr.Zero)
			Path = Path + IntPtrToString(fname);

		byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Path);
		int wrote = 0;
		for (int i = 0; i < bytes.Length && i < len; i++, wrote++)
			Marshal.WriteByte(dst, i, bytes[i]);
		Marshal.WriteByte(dst, wrote, 0);

		gcHandleDir.Free();
		gcHandleFile.Free();
		gcHandleDst.Free();
		GC.KeepAlive(dir);
		GC.KeepAlive(fname);
		GC.KeepAlive(dst);
		GC.KeepAlive(dummy);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_notify_image_update))]
	static unsafe void notify_image_update([Out] IntPtr img)
	{
		GCHandle gcHandle = GCHandle.Alloc(img, GCHandleType.Pinned);

		Image Image = Marshal.PtrToStructure<SuikaEngine.Image>(img);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_notify_image_free))]
	static unsafe void notify_image_free([Out] IntPtr img)
	{
		GCHandle gcHandle = GCHandle.Alloc(img, GCHandleType.Pinned);

		Image Image = Marshal.PtrToStructure<SuikaEngine.Image>(img);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_render_image_normal))]
	static unsafe void render_image_normal(int dst_left, int dst_top, int dst_width, int dst_height, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		GCHandle gcHandle = GCHandle.Alloc(src_img, GCHandleType.Pinned);

		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(src_img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_render_image_add))]
	static unsafe void render_image_add(int dst_left, int dst_top, int dst_width, int dst_height, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		GCHandle gcHandle = GCHandle.Alloc(src_img, GCHandleType.Pinned);

		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(src_img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_render_image_dim))]
	static unsafe void render_image_dim(int dst_left, int dst_top, int dst_width, int dst_height, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		GCHandle gcHandle = GCHandle.Alloc(src_img, GCHandleType.Pinned);

		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(src_img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_render_image_rule))]
	static unsafe void render_image_rule([Out] IntPtr src_img, [Out] IntPtr rule_img, int threshold)
	{
		GCHandle gcHandleSrc = GCHandle.Alloc(src_img, GCHandleType.Pinned);
		GCHandle gcHandleRule = GCHandle.Alloc(rule_img, GCHandleType.Pinned);

		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);
		Image RuleImage = Marshal.PtrToStructure<SuikaEngine.Image>(rule_img);

		// TODO

		gcHandleSrc.Free();
		gcHandleRule.Free();
		GC.KeepAlive(src_img);
		GC.KeepAlive(rule_img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_render_image_melt))]
	static unsafe void render_image_melt([Out] IntPtr src_img, [Out] IntPtr rule_img, int progress)
	{
		GCHandle gcHandleSrc = GCHandle.Alloc(src_img, GCHandleType.Pinned);
		GCHandle gcHandleRule = GCHandle.Alloc(rule_img, GCHandleType.Pinned);

		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);
		Image RuleImage = Marshal.PtrToStructure<SuikaEngine.Image>(rule_img);

		// TODO

		gcHandleSrc.Free();
		gcHandleRule.Free();
		GC.KeepAlive(src_img);
		GC.KeepAlive(rule_img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_render_image_3d_normal))]
	static unsafe void render_image_3d_normal(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		GCHandle gcHandle = GCHandle.Alloc(src_img, GCHandleType.Pinned);

		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(src_img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_render_image_3d_add))]
	static unsafe void render_image_3d_add(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, [Out] IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		GCHandle gcHandle = GCHandle.Alloc(src_img, GCHandleType.Pinned);

		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(src_img);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_reset_lap_timer))]
	static unsafe void reset_lap_timer([Out] IntPtr origin)
	{
		GCHandle gcHandle = GCHandle.Alloc(origin, GCHandleType.Pinned);

		Marshal.WriteInt64(origin, Environment.TickCount);

		gcHandle.Free();
		GC.KeepAlive(origin);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_get_lap_timer_millisec))]
	static unsafe Int64 get_lap_timer_millisec([Out] IntPtr origin)
	{
		GCHandle gcHandle = GCHandle.Alloc(origin, GCHandleType.Pinned);

		Int64 ret = Environment.TickCount - Marshal.ReadInt64(origin);

		gcHandle.Free();
		GC.KeepAlive(origin);

		return ret;
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_play_sound))]
	static unsafe void play_sound(int stream, [Out] IntPtr wave)
	{
		GCHandle gcHandle = GCHandle.Alloc(wave, GCHandleType.Pinned);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(wave);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_stop_sound))]
	static unsafe void stop_sound(int stream)
	{
		// TODO
	}
	
	[AOT.MonoPInvokeCallback(typeof(delegate_set_sound_volume))]
	static unsafe void set_sound_volume(int stream, float vol)
	{
		// TODO
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_is_sound_finished))]
	static unsafe int is_sound_finished(int stream)
	{
		return 1;
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_play_video))]
	static unsafe int play_video([Out] IntPtr fname, int is_skippable)
	{
		GCHandle gcHandle = GCHandle.Alloc(fname, GCHandleType.Pinned);

		// TODO
		gcHandle.Free();
		GC.KeepAlive(fname);

		return 1;
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_stop_video))]
	static unsafe void stop_video()
	{
		// TODO
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_is_video_playing))]
	static unsafe int is_video_playing()
	{
		// TODO
		return 0;
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_update_window_title))]
	static unsafe void update_window_title()
	{
		// TODO
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_is_full_screen_supported))]
	static unsafe int is_full_screen_supported()
	{
		// TODO
		return 0;
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_is_full_screen_mode))]
	static unsafe int is_full_screen_mode()
	{
		// TODO
		return 0;
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_enter_full_screen_mode))]
	static unsafe void enter_full_screen_mode()
	{
		// TODO
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_leave_full_screen_mode))]
	static unsafe void leave_full_screen_mode()
	{
		// TODO
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_get_system_locale))]
	static unsafe IntPtr get_system_locale()
	{
		// TODO
		if (locale == IntPtr.Zero)
		{
			locale = Marshal.StringToCoTaskMemUTF8("ja");
			GCHandle.Alloc(locale, GCHandleType.Pinned);
			GC.KeepAlive(locale);
		}

		return locale;
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_speak_text))]
	static unsafe void speak_text([In] IntPtr text)
	{
		GCHandle gcHandle = GCHandle.Alloc(text, GCHandleType.Pinned);

		// TODO

		gcHandle.Free();
		GC.KeepAlive(text);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_set_continuous_swipe_enabled))]
	static unsafe void set_continuous_swipe_enabled(int is_enabled)
	{
		// TODO
	}

	//
	// HAL helpers
	//

	static unsafe string SaveFile;
	static unsafe string SaveData;

    [AOT.MonoPInvokeCallback(typeof(delegate_free_shared))]
	static unsafe void free_shared([Out] IntPtr p)
	{
		GCHandle gcHandle = GCHandle.Alloc(p, GCHandleType.Pinned);

		Marshal.FreeCoTaskMem(p);

		gcHandle.Free();
		GC.KeepAlive(p);
	}

    [AOT.MonoPInvokeCallback(typeof(delegate_get_file_contents))]
    static unsafe IntPtr get_file_contents([In] IntPtr pFileName, [In] IntPtr len)
	{
		GCHandle gcHandleFile = GCHandle.Alloc(pFileName, GCHandleType.Pinned);
		GCHandle gcHandleLen = GCHandle.Alloc(len, GCHandleType.Pinned);
		GC.KeepAlive(pFileName);

		IntPtr ret = IntPtr.Zero;
		string FileName = IntPtrToString(pFileName);
		if (FileName.StartsWith("sav/"))
		{
			string s = PlayerPrefs.GetString(FileName.Split("/")[1], "");
			if (s == "")
				return IntPtr.Zero;
			byte[] b = Convert.FromBase64String(s);
			if (b == null)
				return IntPtr.Zero;
			ret = Marshal.AllocCoTaskMem(b.Length);
			GC.KeepAlive(ret);
			Marshal.Copy(b, 0, ret, b.Length);
			Marshal.WriteIntPtr(len, 0, (IntPtr)b.Length);
		}
		else
		{
			Debug.Log(Application.streamingAssetsPath);
			Debug.Log(FileName);
			byte[] b = System.IO.File.ReadAllBytes(Application.streamingAssetsPath + "/" + FileName);
			if (b == null)
				return IntPtr.Zero;
			ret = Marshal.AllocCoTaskMem(b.Length);
			GC.KeepAlive(ret);
			Marshal.Copy(b, 0, ret, b.Length);
			Marshal.WriteIntPtr(len, 0, (IntPtr)b.Length);
		}

		gcHandleFile.Free();
		gcHandleLen.Free();
		GC.KeepAlive(pFileName);
		GC.KeepAlive(len);
		GC.KeepAlive(ret);
		return ret;
    }

    private static unsafe string IntPtrToString([In] IntPtr s)
	{
		byte *src = (byte *)s.ToPointer();
		byte *b = src;
		int len = 0;
		while (*b != 0)
		{
			len++;
			b++;
		}
		byte[] managed = new byte[len];
		for (int i = 0; i < len; i++)
		{
			managed[i] = Marshal.ReadByte(s, i);
		}
		string ret = Encoding.UTF8.GetString(managed);
		Debug.Log(ret);
		GC.KeepAlive(s);
		GC.KeepAlive(ret);
		return ret;
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_open_save_file))]
    static unsafe void open_save_file([In] IntPtr pFileName) {
		GCHandle gcHandle = GCHandle.Alloc(pFileName, GCHandleType.Pinned);

		SaveFile = Marshal.PtrToStringUTF8(pFileName).Split("/")[1];
		SaveData = "";

		gcHandle.Free();
		GC.KeepAlive(pFileName);
	}

	[AOT.MonoPInvokeCallback(typeof(delegate_write_save_file))]
    static unsafe void write_save_file(int b) {
		byte[] InArray = new byte[1];
		char[] OutArray = new char[1];
		InArray[0] = 1;
		SaveData = SaveData + Convert.ToBase64CharArray(InArray, 0, 1, OutArray, 0, 0).ToString();
    }

	[AOT.MonoPInvokeCallback(typeof(delegate_close_save_file))]
    static unsafe void close_save_file() {
		string s = PlayerPrefs.GetString(SaveFile, SaveData);
    }
}

/*
		string filename = "Assets/Resources/ch/001-happy.png";
		var rawData = System.IO.File.ReadAllBytes(filename);

		Texture2D tex = new Texture2D(2, 2);
		tex.LoadImage(rawData);
		if (tex == null)
			Debug.Log("NG");
		else
			Debug.Log("OK");
 */
