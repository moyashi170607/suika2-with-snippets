/* -*- coding: utf-8; tab-width: 4; c-basic-offset: 4; indent-tabs-mode: t; -*- */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SuikaEngine : MonoBehaviour
{
	delegate void delegate_log_info(IntPtr s);
	delegate void delegate_log_warn(IntPtr s);
	delegate void delegate_log_error(IntPtr s);
	delegate void delegate_make_sav_dir();
	delegate void delegate_make_valid_path(IntPtr dir, IntPtr fname, IntPtr dst, int len);
	delegate void delegate_notify_image_update(IntPtr img);
	delegate void delegate_notify_image_free(IntPtr img);
	delegate void delegate_render_image_normal(int dst_left, int dst_top, int dst_width, int dst_height, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	delegate void delegate_render_image_add(int dst_left, int dst_top, int dst_width, int dst_height, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	delegate void delegate_render_image_dim(int dst_left, int dst_top, int dst_width, int dst_height, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	delegate void delegate_render_image_rule(IntPtr src_img, IntPtr rule_img, int threshold);
	delegate void delegate_render_image_melt(IntPtr src_img, IntPtr rule_img, int progress);
	delegate void delegate_render_image_3d_normal(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	delegate void delegate_render_image_3d_add(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
	delegate void delegate_reset_lap_timer(IntPtr origin);
	delegate Int64 delegate_get_lap_timer_millisec(IntPtr origin);
	delegate void delegate_play_sound(int stream, IntPtr wave);
	delegate void delegate_stop_sound(int stream);
	delegate void delegate_set_sound_volume(int stream, float vol);
	delegate bool delegate_is_sound_finished(int stream);
	delegate bool delegate_play_video(IntPtr fname, bool is_skippable);
	delegate void delegate_stop_video();
	delegate bool delegate_is_video_playing();
	delegate void delegate_update_window_title();
	delegate bool delegate_is_full_screen_supported();
	delegate bool delegate_is_full_screen_mode();
	delegate void delegate_enter_full_screen_mode();
	delegate void delegate_leave_full_screen_mode();
	delegate IntPtr delegate_get_system_locale();
	delegate void delegate_speak_text(IntPtr text);
	delegate void delegate_set_continuous_swipe_enabled(bool is_enabled);

    void Start()
    {
		init_hal_func_table((IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_log_info(log_info)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_log_warn(log_warn)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_log_error(log_error)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_make_sav_dir(make_sav_dir)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_make_valid_path(make_valid_path)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_notify_image_update(notify_image_update)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_notify_image_free(notify_image_free)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_render_image_normal(render_image_normal)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_render_image_add(render_image_add)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_render_image_dim(render_image_dim)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_render_image_rule(render_image_rule)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_render_image_melt(render_image_melt)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_render_image_3d_normal(render_image_3d_normal)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_render_image_3d_add(render_image_3d_add)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_reset_lap_timer(reset_lap_timer)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_get_lap_timer_millisec(get_lap_timer_millisec)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_play_sound(play_sound)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_stop_sound(stop_sound)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_set_sound_volume(set_sound_volume)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_is_sound_finished(is_sound_finished)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_play_video(play_video)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_stop_video(stop_video)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_is_video_playing(is_video_playing)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_update_window_title(update_window_title)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_is_full_screen_supported(is_full_screen_supported)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_is_full_screen_mode(is_full_screen_mode)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_enter_full_screen_mode(enter_full_screen_mode)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_leave_full_screen_mode(leave_full_screen_mode)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_get_system_locale(get_system_locale)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_speak_text(speak_text)),
							(IntPtr)Marshal.GetFunctionPointerForDelegate(new delegate_set_continuous_swipe_enabled(set_continuous_swipe_enabled)));

		int ret = on_event_init();
		if (ret != 0)
			Debug.Log("OK");
		else
			Debug.Log("NG");
	}

    void Update()
    {
    }

	//
	// Native
	//

    [DllImport("libpolaris")]
    static extern void init_hal_func_table(
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
		IntPtr set_continuous_swipe_enabled
	);

    [DllImport("libpolaris")]
	static extern int on_event_init();

    [DllImport("libpolaris")]
    static extern void on_event_cleanup();

    [DllImport("libpolaris")]
	static extern bool on_event_frame();

    [DllImport("libpolaris")]
    static extern void on_event_key_press(int key);

    [DllImport("libpolaris")]
    static extern void on_event_key_release(int key);

    [DllImport("libpolaris")]
    static extern void on_event_mouse_press(int button, int x, int y);

    [DllImport("libpolaris")]
    static extern void on_event_mouse_release(int button, int x, int y);

    [DllImport("libpolaris")]
    static extern void on_event_mouse_move(int x, int y);

    [DllImport("libpolaris")]
    static extern void on_event_touch_cancel();

    [DllImport("libpolaris")]
    static extern void on_event_swipe_down();

    [DllImport("libpolaris")]
    static extern void on_event_swipe_up();
	
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

	private IntPtr locale = IntPtr.Zero;

	static void log_info(IntPtr s)
	{
		string str = Marshal.PtrToStringUTF8(s);
		Debug.Log(str);
	}

	static void log_warn(IntPtr s)
	{
		string str = Marshal.PtrToStringUTF8(s);
		Debug.Log(str);
	}

	static void log_error(IntPtr s)
	{
		string str = Marshal.PtrToStringUTF8(s);
		Debug.Log(str);
	}

	static void make_sav_dir()
	{
		// TODO
	}

	static void make_valid_path(IntPtr dir, IntPtr fname, IntPtr dst, int len)
	{
		string Path = "";
		if (dir != IntPtr.Zero)
			Path = Path + Marshal.PtrToStringUTF8(dir) + "/";
		if (fname != IntPtr.Zero)
			Path = Path + Marshal.PtrToStringUTF8(fname);

		var bytes = System.Text.Encoding.UTF8.GetBytes(Path);
		for (int i = 0; i < bytes.Length && i < len; i++)
			Marshal.WriteByte(dst , i, bytes[i]);
	}

	static void notify_image_update(IntPtr img)
	{
		Image Image = Marshal.PtrToStructure<SuikaEngine.Image>(img);

		// TODO
	}

	static void notify_image_free(IntPtr img)
	{
		Image Image = Marshal.PtrToStructure<SuikaEngine.Image>(img);

		// TODO
	}

	static void render_image_normal(int dst_left, int dst_top, int dst_width, int dst_height, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO
	}

	static void render_image_add(int dst_left, int dst_top, int dst_width, int dst_height, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO
	}

	static void render_image_dim(int dst_left, int dst_top, int dst_width, int dst_height, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO
	}

	static void render_image_rule(IntPtr src_img, IntPtr rule_img, int threshold)
	{
		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);
		Image RuleImage = Marshal.PtrToStructure<SuikaEngine.Image>(rule_img);

		// TODO
	}

	static void render_image_melt(IntPtr src_img, IntPtr rule_img, int progress)
	{
		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);
		Image RuleImage = Marshal.PtrToStructure<SuikaEngine.Image>(rule_img);

		// TODO
	}

	static void render_image_3d_normal(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO
	}

	static void render_image_3d_add(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, IntPtr src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
	{
		Image SrcImage = Marshal.PtrToStructure<SuikaEngine.Image>(src_img);

		// TODO
	}

	static void reset_lap_timer(IntPtr origin)
	{
		Marshal.WriteInt64(origin, Environment.TickCount);
	}

	Int64 get_lap_timer_millisec(IntPtr origin)
	{
		return Environment.TickCount - Marshal.ReadInt64(origin);
	}

	void play_sound(int stream, IntPtr wave)
	{
		// TODO
	}

	void stop_sound(int stream)
	{
		// TODO
	}
	
	void set_sound_volume(int stream, float vol)
	{
		// TODO
	}

	bool is_sound_finished(int stream)
	{
		return true;
	}

	bool play_video(IntPtr fname, bool is_skippable)
	{
		// TODO
		return true;
	}

	void stop_video()
	{
		// TODO
	}

	bool is_video_playing()
	{
		// TODO
		return false;
	}

	void update_window_title()
	{
		// TODO
	}

	bool is_full_screen_supported()
	{
		// TODO
		return false;
	}

	bool is_full_screen_mode()
	{
		// TODO
		return false;
	}

	void enter_full_screen_mode()
	{
		// TODO
	}

	void leave_full_screen_mode()
	{
		// TODO
	}

	IntPtr get_system_locale()
	{
		// TODO
		if (locale == IntPtr.Zero)
			locale = Marshal.StringToCoTaskMemUTF8("ja");
		return locale;
	}

	void speak_text(IntPtr text)
	{
		// TODO
	}

	void set_continuous_swipe_enabled(bool is_enabled)
	{
		// TODO
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
