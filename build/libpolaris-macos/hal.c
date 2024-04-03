/* -*- coding: utf-8; tab-width: 8; indent-tabs-mode: t; -*- */

/*
 * Suika2
 * Copyright (C) 2001-2024, Keiichi Tabata. All rights reserved.
 */

/*
 * HAL API wrapper for Unity
 */

#include "suika.h"

bool (*log_info)(const char *s, ...);
bool (*log_warn)(const char *s, ...);
bool (*log_error)(const char *s, ...);
bool (*make_sav_dir)(void);
char *(*make_valid_path)(const char *dir, const char *fname);
void (*notify_image_update)(struct image *img);
void (*notify_image_free)(struct image *img);
void (*render_image_normal)(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*render_image_add)(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*render_image_dim)(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*render_image_rule)(struct image *src_img, struct image *rule_img, int threshold);
void (*render_image_melt)(struct image *src_img, struct image *rule_img, int progress);
void (*render_image_3d_normal)(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*render_image_3d_add)(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*reset_lap_timer)(uint64_t *origin);
uint64_t (*get_lap_timer_millisec)(uint64_t *origin);
bool (*play_sound)(int stream, struct wave *w);
bool (*stop_sound)(int stream);
bool (*set_sound_volume)(int stream, float vol);
bool (*is_sound_finished)(int stream);
bool (*play_video)(const char *fname, bool is_skippable);
void (*stop_video)(void);
bool (*is_video_playing)(void);
void (*update_window_title)(void);
bool (*is_full_screen_supported)(void);
bool (*is_full_screen_mode)(void);
void (*enter_full_screen_mode)(void);
void (*leave_full_screen_mode)(void);
const char *(*get_system_locale)(void);
void (*speak_text)(const char *text);
void (*set_continuous_swipe_enabled)(bool is_enabled);

void
init_hal_func_table
(
    bool (*p_log_info)(const char *s, ...),
    bool (*p_log_warn)(const char *s, ...),
    bool (*p_log_error)(const char *s, ...),
    bool (*p_make_sav_dir)(void),
    char *(*p_make_valid_path)(const char *dir, const char *fname),
    void (*p_notify_image_update)(struct image *img),
    void (*p_notify_image_free)(struct image *img),
    void (*p_render_image_normal)(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha),
    void (*p_render_image_add)(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha),
    void (*p_render_image_dim)(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha),
    void (*p_render_image_rule)(struct image *src_img, struct image *rule_img, int threshold),
    void (*p_render_image_melt)(struct image *src_img, struct image *rule_img, int progress),
    void (*p_render_image_3d_normal)(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha),
    void (*p_render_image_3d_add)(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, struct image *src_image, int src_left, int src_top, int src_width, int src_height, int alpha),
    void (*p_reset_lap_timer)(uint64_t *origin),
    uint64_t (*p_get_lap_timer_millisec)(uint64_t *origin),
    bool (*p_play_sound)(int stream, struct wave *w),
    bool (*p_stop_sound)(int stream),
    bool (*p_set_sound_volume)(int stream, float vol),
    bool (*p_is_sound_finished)(int stream),
    bool (*p_play_video)(const char *fname, bool is_skippable),
    void (*p_stop_video)(void),
    bool (*p_is_video_playing)(void),
    void (*p_update_window_title)(void),
    bool (*p_is_full_screen_supported)(void),
    bool (*p_is_full_screen_mode)(void),
    void (*p_enter_full_screen_mode)(void),
    void (*p_leave_full_screen_mode)(void),
    const char *(*p_get_system_locale)(void),
    void (*p_speak_text)(const char *text),
    void (*p_set_continuous_swipe_enabled)(bool is_enabled)
)
{
	log_info = p_log_info;
	log_warn = p_log_warn;
	log_error = p_log_error;
	make_sav_dir = p_make_sav_dir;
	make_valid_path = p_make_valid_path;
	notify_image_update = p_notify_image_update;
	notify_image_free = p_notify_image_free;
	render_image_normal = p_render_image_normal;
	render_image_add = p_render_image_add;
	render_image_dim = p_render_image_dim;
	render_image_rule = p_render_image_rule;
	render_image_melt = p_render_image_melt;
	render_image_3d_normal = p_render_image_3d_normal;
	render_image_3d_add = p_render_image_3d_add;
	reset_lap_timer = p_reset_lap_timer;
	get_lap_timer_millisec = p_get_lap_timer_millisec;
	play_sound = p_play_sound;
	stop_sound = p_stop_sound;
	set_sound_volume = p_set_sound_volume;
	is_sound_finished = p_is_sound_finished;
	play_video = p_play_video;
	stop_video = p_stop_video;
	is_video_playing = p_is_video_playing;
	update_window_title = p_update_window_title;
	is_full_screen_supported = p_is_full_screen_supported;
	is_full_screen_mode = p_is_full_screen_mode;
	enter_full_screen_mode = p_enter_full_screen_mode;
	leave_full_screen_mode = p_leave_full_screen_mode;
	get_system_locale = p_get_system_locale;
	speak_text = p_speak_text;
	set_continuous_swipe_enabled = p_set_continuous_swipe_enabled;
}
