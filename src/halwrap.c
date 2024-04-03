/* -*- coding: utf-8; tab-width: 8; indent-tabs-mode: t; -*- */

/*
 * Suika2
 * Copyright (C) 2001-2024, Keiichi Tabata. All rights reserved.
 */

/*
 * HAL Wrapper for Foreign Languages
 *  - 2024/04/03 Created for Swift and C#.
 */

#include "suika.h"

/*
 * Function Pointers
 */

void (*wrap_log_info)(intptr_t s);
void (*wrap_log_warn)(intptr_t s);
void (*wrap_log_error)(intptr_t s);
void (*wrap_make_sav_dir)(void);
void (*wrap_make_valid_path)(intptr_t dir, intptr_t fname, intptr_t dst, int len);
void (*wrap_notify_image_update)(intptr_t img);
void (*wrap_notify_image_free)(intptr_t img);
void (*wrap_render_image_normal)(int dst_left, int dst_top, int dst_width, int dst_height, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*wrap_render_image_add)(int dst_left, int dst_top, int dst_width, int dst_height, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*wrap_render_image_dim)(int dst_left, int dst_top, int dst_width, int dst_height, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*wrap_render_image_rule)(intptr_t src_img, intptr_t rule_img, int threshold);
void (*wrap_render_image_melt)(intptr_t src_img, intptr_t rule_img, int progress);
void (*wrap_render_image_3d_normal)(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*wrap_render_image_3d_add)(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha);
void (*wrap_reset_lap_timer)(intptr_t origin);
int64_t (*wrap_get_lap_timer_millisec)(intptr_t origin);
void (*wrap_play_sound)(int stream, intptr_t wave);
void (*wrap_stop_sound)(int stream);
void (*wrap_set_sound_volume)(int stream, float vol);
bool (*wrap_is_sound_finished)(int stream);
bool (*wrap_play_video)(intptr_t fname, bool is_skippable);
void (*wrap_stop_video)(void);
bool (*wrap_is_video_playing)(void);
void (*wrap_update_window_title)(void);
bool (*wrap_is_full_screen_supported)(void);
bool (*wrap_is_full_screen_mode)(void);
void (*wrap_enter_full_screen_mode)(void);
void (*wrap_leave_full_screen_mode)(void);
void (*wrap_get_system_locale)(intptr_t dst, int len);
void (*wrap_speak_text)(intptr_t text);
void (*wrap_set_continuous_swipe_enabled)(bool is_enabled);

/*
 * Initialize
 */

void init_hal_func_table(
	void (*p_log_info)(intptr_t s),
	void (*p_log_warn)(intptr_t s),
	void (*p_log_error)(intptr_t s),
	void (*p_make_sav_dir)(void),
	void (*p_make_valid_path)(intptr_t dir, intptr_t fname, intptr_t dst, int len),
	void (*p_notify_image_update)(intptr_t img),
	void (*p_notify_image_free)(intptr_t img),
	void (*p_render_image_normal)(int dst_left, int dst_top, int dst_width, int dst_height, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha),
	void (*p_render_image_add)(int dst_left, int dst_top, int dst_width, int dst_height, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha),
	void (*p_render_image_dim)(int dst_left, int dst_top, int dst_width, int dst_height, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha),
	void (*p_render_image_rule)(intptr_t src_img, intptr_t rule_img, int threshold),
	void (*p_render_image_melt)(intptr_t src_img, intptr_t rule_img, int progress),
	void (*p_render_image_3d_normal)(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha),
	void (*p_render_image_3d_add)(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, intptr_t src_img, int src_left, int src_top, int src_width, int src_height, int alpha),
	void (*p_reset_lap_timer)(intptr_t origin),
	int64_t (*p_get_lap_timer_millisec)(intptr_t origin),
	void (*p_play_sound)(int stream, intptr_t wave),
	void (*p_stop_sound)(int stream),
	void (*p_set_sound_volume)(int stream, float vol),
	bool (*p_is_sound_finished)(int stream),
	bool (*p_play_video)(intptr_t fname, bool is_skippable),
	void (*p_stop_video)(void),
	bool (*p_is_video_playing)(void),
	void (*p_update_window_title)(void),
	bool (*p_is_full_screen_supported)(void),
	bool (*p_is_full_screen_mode)(void),
	void (*p_enter_full_screen_mode)(void),
	void (*p_leave_full_screen_mode)(void),
	void (*p_get_system_locale)(intptr_t dst, int len),
	void (*p_speak_text)(intptr_t text),
	void (*p_set_continuous_swipe_enabled)(bool is_enabled))
{
	wrap_log_info = p_log_info;
	wrap_log_warn = p_log_warn;
	wrap_log_error = p_log_error;
	wrap_make_sav_dir = p_make_sav_dir;
	wrap_make_valid_path = p_make_valid_path;
	wrap_notify_image_update = p_notify_image_update;
	wrap_notify_image_free = p_notify_image_free;
	wrap_render_image_normal = p_render_image_normal;
	wrap_render_image_add = p_render_image_add;
	wrap_render_image_dim = p_render_image_dim;
	wrap_render_image_rule = p_render_image_rule;
	wrap_render_image_melt = p_render_image_melt;
	wrap_render_image_3d_normal = p_render_image_3d_normal;
	wrap_render_image_3d_add = p_render_image_3d_add;
	wrap_reset_lap_timer = p_reset_lap_timer;
	wrap_get_lap_timer_millisec = p_get_lap_timer_millisec;
	wrap_play_sound = p_play_sound;
	wrap_stop_sound = p_stop_sound;
	wrap_set_sound_volume = p_set_sound_volume;
	wrap_is_sound_finished = p_is_sound_finished;
	wrap_play_video = p_play_video;
	wrap_stop_video = p_stop_video;
	wrap_is_video_playing = p_is_video_playing;
	wrap_update_window_title = p_update_window_title;
	wrap_is_full_screen_supported = p_is_full_screen_supported;
	wrap_is_full_screen_mode = p_is_full_screen_mode;
	wrap_enter_full_screen_mode = p_enter_full_screen_mode;
	wrap_leave_full_screen_mode = p_leave_full_screen_mode;
	wrap_get_system_locale = p_get_system_locale;
	wrap_speak_text = p_speak_text;
	wrap_set_continuous_swipe_enabled = p_set_continuous_swipe_enabled;
}

/*
 * Wrappers
 */

bool log_info(const char *s, ...)
{
	char buf[4096];
	va_list ap;

	va_start(ap, s);
	vsnprintf(buf, sizeof(buf), s, ap);
	va_end(ap);

	wrap_log_info((intptr_t)buf);

	return true;
}

bool log_warn(const char *s, ...)
{
	char buf[4096];
	va_list ap;

	va_start(ap, s);
	vsnprintf(buf, sizeof(buf), s, ap);
	va_end(ap);

	wrap_log_info((intptr_t)buf);

	return true;
}

bool log_error(const char *s, ...)
{
	char buf[4096];
	va_list ap;

	va_start(ap, s);
	vsnprintf(buf, sizeof(buf), s, ap);
	va_end(ap);

	wrap_log_info((intptr_t)buf);

	return true;
}

bool make_sav_dir(void)
{
	wrap_make_sav_dir();
	return true;
}

char *make_valid_path(const char *dir, const char *fname)
{
	char buf[4096];
	char *ret;

	wrap_make_valid_path((intptr_t)dir, (intptr_t)fname, (intptr_t)buf, sizeof(buf));

	ret = strdup(buf);
	if (ret == NULL) {
		log_memory();
		return NULL;
	}
	return ret;
}

void notify_image_update(struct image *img)
{
	wrap_notify_image_update((intptr_t)img);
}

void notify_image_free(struct image *img)
{
	wrap_notify_image_free((intptr_t)img);
}

void render_image_normal(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
{
	wrap_render_image_normal(dst_left, dst_top, dst_width, dst_height, (intptr_t)src_img, src_left, src_top, src_width, src_height, alpha);
}

void render_image_add(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
{
	wrap_render_image_add(dst_left, dst_top, dst_width, dst_height, (intptr_t)src_img, src_left, src_top, src_width, src_height, alpha);
}

void render_image_dim(int dst_left, int dst_top, int dst_width, int dst_height, struct image *src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
{
    wrap_render_image_dim(dst_left, dst_top, dst_width, dst_height, (intptr_t)src_img, src_left, src_top, src_width, src_height, alpha);
}

void render_image_rule(struct image *src_img, struct image *rule_img, int threshold)
{
	wrap_render_image_rule((intptr_t)src_img, (intptr_t)rule_img, threshold);
}

void render_image_melt(struct image *src_img, struct image *rule_img, int progress)
{
	wrap_render_image_melt((intptr_t)src_img, (intptr_t)rule_img, progress);
}

void render_image_3d_normal(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, struct image *src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
{
	wrap_render_image_3d_normal(x1, y1, x2, y2, x3, y3, x4, y4, (intptr_t)src_img, src_left, src_top, src_width, src_height, alpha);
}

void render_image_3d_add(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, struct image *src_img, int src_left, int src_top, int src_width, int src_height, int alpha)
{
	wrap_render_image_3d_add(x1, y1, x2, y2, x3, y3, x4, y4, (intptr_t)src_img, src_left, src_top, src_width, src_height, alpha);
}

void reset_lap_timer(uint64_t *origin)
{
	wrap_reset_lap_timer((intptr_t)origin);
}

uint64_t get_lap_timer_millisec(uint64_t *origin)
{
	uint64_t ret;
	ret = wrap_get_lap_timer_millisec((intptr_t)origin);
	return ret;
}

bool play_sound(int stream, struct wave *w)
{
	wrap_play_sound(stream, (intptr_t)w);
	return true;
}

bool stop_sound(int stream)
{
	wrap_stop_sound(stream);
	return true;
}

bool set_sound_volume(int stream, float vol)
{
	wrap_set_sound_volume(stream, vol);
	return true;
}

bool is_sound_finished(int stream)
{
	bool ret;
	ret =  wrap_is_sound_finished(stream);
	return ret;
}

bool play_video(const char *fname, bool is_skippable)
{
	bool ret;
	ret =  wrap_play_video((intptr_t)fname, is_skippable);
	return true;
}

void stop_video(void)
{
	wrap_stop_video();
}

bool is_video_playing(void)
{
	bool ret;
	ret = wrap_is_video_playing();
	return ret;
}

void update_window_title(void)
{
	wrap_update_window_title();
}

bool is_full_screen_supported(void)
{
	bool ret;
	ret = wrap_is_full_screen_supported();
	return ret;
}

bool is_full_screen_mode(void)
{
	bool ret;
	ret = wrap_is_full_screen_mode();
	return ret;
}

void enter_full_screen_mode(void)
{
	wrap_enter_full_screen_mode();
}

void leave_full_screen_mode(void)
{
	wrap_leave_full_screen_mode();
}

const char *get_system_locale(void)
{
	static char buf[64];

	wrap_get_system_locale((intptr_t)buf, sizeof(buf));

	return buf;
}

void speak_text(const char *text)
{
	wrap_speak_text((intptr_t)text);
}

#if defined(SUIKA_TARGET_IOS) || defined(SUIKA_TARGET_ANDROID) || defined(SUIKA_TARGET_WASM)
void set_continuous_swipe_enabled(bool is_enabled)
{
	wrap_set_continuous_swipe_enabled(is_enabled);
}
#endif
