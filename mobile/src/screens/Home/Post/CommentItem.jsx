import { StyleSheet, Text, View } from "react-native";
import React from "react";

const CommentItem = ({data}) => {
  return (
    <View>
      <Text>{data.value}</Text>
    </View>
  );
};

export default CommentItem;

const styles = StyleSheet.create({});
