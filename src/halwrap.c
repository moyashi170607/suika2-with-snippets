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
void (*wrap_make_valid_path)(intptr_t dir, intptr_t fname, intptr_rt dst, int len);
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
intptr_t (*wrap_get_system_locale)(void);
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
	void (*p_make_valid_path)(intptr_t dir, intptr_t fname, intptr_rt dst, int len),
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
	intptr_t (*p_get_system_locale)(void),
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

	wrap_log_info(buf);
}

bool log_warn(const char *s, ...)
{
	char buf[4096];
	va_list ap;

	va_start(ap, s);
	vsnprintf(buf, sizeof(buf), s, ap);
	va_end(ap);

	wrap_log_info(buf);
}

bool log_error(const char *s, ...)
{
	char buf[4096];
	va_list ap;

	va_start(ap, s);
	vsnprintf(buf, sizeof(buf), s, ap);
	va_end(ap);

	wrap_log_info(buf);
}

bool make_sav_dir(void)
{
}

/*
 * Creates an effective path string from a directory name and a file name.
 *  - This function absorbs OS-specific path handling
 *  - Resulting strings can be passed to open_rfile() and open_wfile()
 *  - Resulting strings must be free()-ed by callers
 */
char *make_valid_path(const char *dir, const char *fname);

/************************
 * Texture Manipulation *
 ************************/

/*
 * Notifies an image update.
 *  - This function tells a HAL that an image needs to be uploaded to GPU
 *  - A HAL can upload images to GPU at an appropriate time
 */
void notify_image_update(struct image *img);

/*
 * Notifies an image free.
 *  - This function tells a HAL that an image is no longer used
 *  - This function must be called from destroy_image() only
 */
void notify_image_free(struct image *img);

/*
 * Returns if RGBA values have to be reversed to BGRA.
 */
#if defined(SUIKA_TARGET_ANDROID) || defined(SUIKA_TARGET_WASM) || defined(SUIKA_TARGET_POSIX) || defined(USE_QT)
#define is_opengl_byte_order()	true
#else
#define is_opengl_byte_order()	false
#endif

/*************
 * Rendering *
 *************/

/*
 * Renders an image to the screen with the "normal" shader pipeline.
 *  - The "normal" shader pipeline renders pixels with alpha blending
 */
void
render_image_normal(
	int dst_left,			/* The X coordinate of the screen */
	int dst_top,			/* The Y coordinate of the screen */
	int dst_width,			/* The width of the destination rectangle */
	int dst_height,			/* The width of the destination rectangle */
	struct image *src_image,	/* [IN] The image to be rendered */
	int src_left,			/* The X coordinate of a source image */
	int src_top,			/* The Y coordinate of a source image */
	int src_width,			/* The width of the source rectangle */
	int src_height,			/* The height of the source rectangle */
	int alpha);			/* The alpha value (0 to 255) */

/*
 * Renders an image to the screen with the "normal" shader pipeline.
 *  - The "normal" shader pipeline renders pixels with alpha blending
 */
void
render_image_add(
	int dst_left,			/* The X coordinate of the screen */
	int dst_top,			/* The Y coordinate of the screen */
	int dst_width,			/* The width of the destination rectangle */
	int dst_height,			/* The width of the destination rectangle */
	struct image *src_image,	/* [IN] The image to be rendered */
	int src_left,			/* The X coordinate of a source image */
	int src_top,			/* The Y coordinate of a source image */
	int src_width,			/* The width of the source rectangle */
	int src_height,			/* The height of the source rectangle */
	int alpha);			/* The alpha value (0 to 255) */

/*
 * Renders an image to the screen with the "dim" shader pipeline.
 *  - The "dim" shader pipeline renders pixels at 50% values for each RGB component
 */
void
render_image_dim(
	int dst_left,			/* The X coordinate of the screen */
	int dst_top,			/* The Y coordinate of the screen */
	int dst_width,			/* The width of the destination rectangle */
	int dst_height,			/* The width of the destination rectangle */
	struct image *src_image,	/* [IN] The image to be rendered */
	int src_left,			/* The X coordinate of a source image */
	int src_top,			/* The Y coordinate of a source image */
	int src_width,			/* The width of the source rectangle */
	int src_height,			/* The height of the source rectangle */
	int alpha);			/* The alpha value (0 to 255) */

/*
 * Renders an image to the screen with the "rule" shader pipeline.
 *  - The "rule" shader pipeline is a variation of "universal transition" with a threshold value
 *  - A rule image must have the same size as the screen
 */
void
render_image_rule(
	struct image *src_img,	/* [IN] The source image */
	struct image *rule_img,	/* [IN] The rule image */
	int threshold);		/* The threshold (0 to 255) */

/*
 * Renders an image to the screen with the "melt" shader pipeline.
 *  - The "melt" shader pipeline is a variation of "universal transition" with a progress value
 *  - A rule image must have the same size as the screen
 */
void render_image_melt(
	struct image *src_img,	/* [IN] The source image */
	struct image *rule_img,	/* [IN] The rule image */
	int progress);		/* The progress (0 to 255) */

/*
 * Renders an image to the screen as a triangle strip with the "normal" shader pipeline.
 */
void
render_image_3d_normal(
	float x1,
	float y1,
	float x2,
	float y2,
	float x3,
	float y3,
	float x4,
	float y4,
	struct image *src_image,
	int src_left,
	int src_top,
	int src_width,
	int src_height,
	int alpha);

/*
 * Renders an image to the screen as a triangle strip with the "add" shader pipeline.
 */
void
render_image_3d_add(
	float x1,
	float y1,
	float x2,
	float y2,
	float x3,
	float y3,
	float x4,
	float y4,
	struct image *src_image,
	int src_left,
	int src_top,
	int src_width,
	int src_height,
	int alpha);

/*************
 * Lap Timer *
 *************/

/*
 * Resets a lap timer and initializes it with a current time.
 */
void reset_lap_timer(uint64_t *origin);

/*
 * Gets a lap time in milliseconds.
 */
uint64_t get_lap_timer_millisec(uint64_t *origin);

/******************
 * Sound Playback *
 ******************/

/*
 * Note: we have the following sound streams:
 *  - BGM_STREAM:   background music
 *  - SE_STREAM:    sound effect
 *  - VOICE_STREAM: character voice
 *  - SYS_STREAM:   system sound
 */

/*
 * Starts playing a sound file on a track.
 */
bool
play_sound(int stream,		/* A sound stream index */
	   struct wave *w);	/* [IN] A sound object, ownership will be delegated to the callee */

/*
 * Stops playing a sound track.
 */
bool stop_sound(int stream);

/*
 * Sets sound volume.
 */
bool set_sound_volume(int stream, float vol);

/*
 * Returns whether a sound playback for a stream is already finished.
 *  - This function exists to detect voice playback finish
 */
bool is_sound_finished(int stream);

/******************
 * Video Playback *
 ******************/

/*
 * Starts playing a video file.
 */
bool play_video(const char *fname,	/* file name */
		bool is_skippable);	/* allow skip for a unseen video */

/*
 * Stops playing music stream.
 */
void stop_video(void);

/*
 * Returns whether a video playcack is running.
 */
bool is_video_playing(void);

/***********************
 * Window Manipulation *
 ***********************/

/*
 * Updates the window title by configs and the chapter name.
 */
void update_window_title(void);

/*
 * Returns whether the current HAL supports the "full screen mode".
 *  - The "full screen mode" includes docking of some game consoles
 *  - A HAL can implement the "full screen mode" but it is optional
 */
bool is_full_screen_supported(void);

/*
 * Returns whether the current HAL is in the "full screen mode".
 */
bool is_full_screen_mode(void);

/*
 * Enters the full screen mode.
 *  - A HAL can ignore this call
 */
void enter_full_screen_mode(void);

/*
 * Leaves the full screen mode.
 *  - A HAL can ignore this call
 */
void leave_full_screen_mode(void);

/**********
 * Locale *
 **********/

/*
 * Gets the system language.
 *  - Return value can be:
 *    - "ja": Japanese
 *    - "en": English
 *    - "zh": Simplified Chinese
 *    - "tw": Traditional Chinese
 *    - "fr": French
 *    - "it": Italian
 *    - "es": Spanish (Castellano)
 *    - "de": Deutsch
 *    - "el": Greek
 *    - "ru": Russian
 *    - "other": Other (must fallback to English)
 *  - To add a language, make sure to change the following:
 *    - "enum language_code" in conf.h
 *    - init_locale_code() in conf.c
 *    - set_locale_mapping() in conf.c
 *    - get_ui_message() in uimsg.c
 *  - Note that:
 *    - Currently we don't have a support for right-to-left writing systems
 *      - e.g. Hebrew, Arabic
 *      - It should be implemented in draw_msg_common() in glyph.c
 *      - It can be easily implemented but the author cannot read those characters and need helps
 *    - Glyphs that are composed from multiple Unicode codepoints are not supported
 *      - Currently we need pre-composed texts
 */
const char *get_system_locale(void);

/******************
 * Text-To-Speech *
 ******************/

/*
 * Speaks a text.
 *  - Specifying NULL stops speaking.
 */
void speak_text(const char *text);
