/* -*- coding: utf-8; tab-width: 8; indent-tabs-mode: t; -*- */

/*
 * Suika2 / Polaris Engine
 * Copyright (C) 2001-2021, Keiichi Tabata. All rights reserved.
 */

/*
 * [Changes]
 *  - 2016/07/02 作成
 *  - 2021/06/06 ボイスストリームでの再生に対応
 */

#include "suika.h"

/*
 * seコマンド
 *  - 2022/5/10 loopオプションを追加
 */
bool se_command(void)
{
	struct wave *w;
	const char *fname;
	const char *option;
	int stream;
	bool loop, stop;

	/* パラメータを取得する */
	fname = get_string_param(SE_PARAM_FILE);
	option = get_string_param(SE_PARAM_OPTION);

	/* 停止の指示かどうかチェックする */
	if (strcmp(fname, "stop") == 0 || strcmp(fname, U8("停止")) == 0)
		stop = true;
	else
		stop = false;

	/*
	 * 1. voice指示の有無を確認する
	 *  - マスターボリュームのフィードバック再生を行う際、テキスト表示なし
	 *    でボイスを再生できるようにするための指示
	 * 2. ループ再生するかを確認する
	 */
	if (strcmp(option, "voice") == 0) {
		stream = VOICE_STREAM;
		loop = false;
	} else if (strcmp(option, "loop") == 0) {
		stream = SE_STREAM;
		loop = true;
	} else {
		stream = SE_STREAM;
		loop = false;
	}

	/* 停止の指示でない場合 */
	if (!stop) {
		/* PCMストリームをオープンする */
		w = create_wave_from_file(SE_DIR, fname, loop);
		if (w == NULL) {
			log_script_exec_footer();
			return false;
		}
	} else {
		w = NULL;
	}

	/* ループ再生のときはSEファイル名を登録する */
	if (!stop && loop) {
		if (!set_se_file_name(fname))
			return false;
	} else {
		set_se_file_name(NULL);
	}

	/* 再生を開始する */
	set_mixer_input(stream, w);

	/* 次のコマンドへ移動する */
	return move_to_next_command();
}
