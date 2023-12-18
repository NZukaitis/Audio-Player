package com.example.audio_player

import android.annotation.SuppressLint
import android.os.Build
import android.os.Bundle
import android.widget.ScrollView
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.annotation.RequiresApi
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.LazyItemScope
import androidx.compose.foundation.lazy.LazyListScope
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.verticalScroll
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.rounded.Search
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.ScrollableTabRow
import androidx.compose.material3.Surface
import androidx.compose.material3.Tab
import androidx.compose.material3.TabRowDefaults
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.material3.contentColorFor
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.unit.dp
import com.example.audio_player.ui.theme.AudioPlayerTheme
import java.io.File
import java.nio.file.Files
import java.nio.file.Paths


class MainActivity : ComponentActivity() {
    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            AudioPlayerTheme {
                // A surface container using the 'background' color from the theme
                Surface(modifier = Modifier.fillMaxSize(), color = MaterialTheme.colorScheme.background) {
                    Column{
                        topBar()
                        tabAndContents()
                    }

                }
            }
        }
    }
}


@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun topBar(){
    var searchString by remember { mutableStateOf("")}
    var isActive by remember { mutableStateOf(false)}

    val contextForToast = LocalContext.current.applicationContext

    Row {
    //Search Bar
        TextField(value = searchString,
            onValueChange = {searchString = it},
            leadingIcon = {Icon(
                Icons.Rounded.Search,
                contentDescription = null)},
            shape = RoundedCornerShape(8.dp),
            colors = TextFieldDefaults.textFieldColors(
                disabledTextColor = Color.Transparent,
                focusedIndicatorColor = Color.Transparent,
                unfocusedIndicatorColor = Color.Transparent,
                disabledIndicatorColor = Color.Transparent
            ),
            singleLine = true,
            modifier = Modifier
                .padding(10.dp)
                .fillMaxWidth())


    }
}

@RequiresApi(Build.VERSION_CODES.O)
@Composable
fun tabAndContents() {
    var tabIndex by remember {
        mutableStateOf(0)
    }

    val tabRowItems = listOf(
        "Artists", "Albums", "Tracks", "Playlists", "EQ", "Settings"
    )

    var currentScreen by remember {
        mutableStateOf("Artists")
        //artists is the default page for now
    }

    Column(modifier = Modifier.verticalScroll(rememberScrollState())) {
        ScrollableTabRow(
            selectedTabIndex = tabIndex,
            containerColor = TabRowDefaults.containerColor,
            contentColor = TabRowDefaults.contentColor, edgePadding = 0.dp
        ) {

            tabRowItems.forEachIndexed { index, value ->
                Tab(selected = tabIndex == index, onClick = {
                    currentScreen = value
                    tabIndex = index
                }, text = { Text(text = value) })
            }


        }
            for(item in changeContent(currentScreen)){
                println(item)
                Text(text = item)
        }
    }
}

@SuppressLint("SdCardPath")
@RequiresApi(Build.VERSION_CODES.O)
fun changeContent(page: String): MutableList<String> {  //this is horribly slow, will do faster way later
    when(page){
        "Artists" -> {
            var artists = mutableListOf("")
            val list = File("/sdcard/Music").list()

            for (item in list) {
                if (artists[0] == "") {
                    artists[0] = item
                    continue
                }
                artists.add(item)
            }
            return artists
        }
        "Albums" -> {
            var albums = mutableListOf("")
            val artists = File("/sdcard/Music").list()
            val list = mutableListOf("")

            for(item in artists){
                if(item == ".thumbnails") continue
                for(album in File("/sdcard/Music/" + item).list()){
                    if(albums[0] == ""){
                        albums[0] = album
                        continue
                    }

                    albums.add(album)
                }
            }

            return albums
        }
        "Tracks" -> { //TODO do tracks correctly
            val tracks = mutableListOf("")
            Files.walk(Paths.get("/sdcard/Music")).use {
                    paths -> paths.filter { Files.isRegularFile(it) }
            }

            return tracks
        }
        "Playlists" ->return mutableListOf("") //TODO add UI for remaining pages
        "EQ" ->return mutableListOf("")
        "Settings" ->return mutableListOf("")
    }
    return mutableListOf("")
}


