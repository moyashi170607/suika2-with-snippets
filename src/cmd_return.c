/* -*- coding: utf-8; tab-width: 8; indent-tabs-mode: t; -*- */

/*
 * Suika2 / Polaris Engine
 * Copyright (C) 2001-2018, Keiichi Tabata. All rights reserved.
 */

/*
 * [Changes]
 *  - 2018/07/22 作成
 */

#include "suika.h"

/*
 * returnコマンド
 */
bool return_command(void)
{
	int rp;

	/* リターンポイントを取得する */
	rp = pop_return_point();

	/*
	 * リターンポイントが無効な場合、エラーとする
	 *  - 最初の行でpushされると-1になる点に留意
	 */
	if (rp < -1) {
		/* エラーを出力する */
		log_script_return_error();
		log_script_exec_footer();
		return false;
	}

	/* リターンポイントの次の行に復帰する */
	if (!move_to_command_index(rp + 1)) {
		/* エラーを出力する */
		log_script_return_error();
		log_script_exec_footer();
		return false;
	}

	return true;
}
