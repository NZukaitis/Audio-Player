package com.example.audio_player

import android.os.Bundle
import android.widget.SearchView
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.rounded.Search
import androidx.compose.material.icons.rounded.Settings
import androidx.compose.material.icons.rounded.ShoppingCart
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.ScrollableTabRow
import androidx.compose.material3.Surface
import androidx.compose.material3.Tab
import androidx.compose.material3.TabRow
import androidx.compose.material3.TabRowDefaults
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.example.audio_player.ui.theme.AudioPlayerTheme



class MainActivity : ComponentActivity() {
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
            modifier = Modifier.padding(10.dp)
                .fillMaxWidth())


    }
}

@Composable
fun tabAndContents(){
    var tabIndex by remember {
        mutableStateOf(0)
    }
    var placeholder by remember {
        mutableStateOf("")
    }

    val tabRowItems = listOf(
        "Artists", "Albums", "Tracks", "Playlists", "EQ", "Settings"
    )

    Column {
        ScrollableTabRow(selectedTabIndex = tabIndex,
            containerColor= TabRowDefaults.containerColor,
            contentColor = TabRowDefaults.contentColor, edgePadding = 0.dp
            ) {

            tabRowItems.forEachIndexed{index, value->
                Tab(selected = tabIndex==index, onClick = {
                    placeholder = value
                    tabIndex = index
                }, text = { Text(text = value)})
            }
        }

        Text(text = placeholder)
    }

}


