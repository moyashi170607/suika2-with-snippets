/* -*- coding: utf-8; tab-width: 8; indent-tabs-mode: t; -*- */

/*
 * Suika2 / Polaris Engine
 * Copyright (C) 2001-2024, Keiichi Tabata. All rights reserved.
 */

/*
 * Anime
 *
 * [Changes]
 *  2023-10-XX Created.
 *  2024-03-03 Added eye anime support.
 */

#ifndef SUIKA_ANIME_H
#define SUIKA_ANIME_H

#include "types.h"
#include "image.h"

/*
 * Maximum amount of registered anime files.
 */
enum reg_anime_index {
	REG_ANIME_COUNT = 16,
};

/*
 * アニメーションの加速
 */
enum anime_accel {
	ANIME_ACCEL_UNIFORM,
	ANIME_ACCEL_ACCEL,
	ANIME_ACCEL_DEACCEL,
};

/* アニメーションサブシステムに関する初期化処理を行う */
bool init_anime(void);

/* アニメーションサブシステムに関する終了処理を行う */
void cleanup_anime(void);

/* アニメーションファイルを読み込む */
bool load_anime_from_file(const char *fname, int reg_index, bool *used_layer);

/* アニメーションシーケンスをクリアする */
void clear_layer_anime_sequence(int layer);
void clear_all_anime_sequence(void);

/* アニメーションシーケンスを開始する */
bool new_anime_sequence(int layer);

/* アニメーションシーケンスにプロパティを追加する */
bool add_anime_sequence_property_f(const char *key, float val);
bool add_anime_sequence_property_i(const char *key, int val);

/* 指定したレイヤのアニメーションを開始する */
bool start_layer_anime(int layer);

/* 指定したレイヤのアニメーションを完了する */
bool finish_layer_anime(int layer);

/* 実行中のアニメーションがあるか調べる */
bool is_anime_running(void);

/* 実行中のアニメーションがあるか調べる */
bool is_anime_running_except_eye_blinking(void);

/* 指定したレイヤーの中にアニメが実行中のものがあるか調べる */
bool is_anime_running_with_layer_mask(bool *used_layers);

/* 指定したレイヤーのアニメーションが実行中であるか調べる */
bool is_anime_finished_for_layer(int layer);

/* アニメーションのフレームを更新する */
void update_anime_frame(void);

/* ループアニメの登録を解除する */
void unregister_anime(int reg_index);

/* ループアニメファイル名を取得する */
const char *get_reg_anime_file_name(int reg_index);

/* 目パチ画像をロードする */
bool load_eye_image_if_exists(int chpos, const char *fname);

#endif
